using Grpc.Core;
using System.Collections.Concurrent;

namespace DiscountServiceGrpc.Services;

public class DiscountService : Discount.DiscountBase
{
    private static readonly ConcurrentDictionary<string, byte> UsedCodesCache = new();
    private static readonly string STORAGE_PATH = "codes.txt";
    private readonly ILogger<DiscountService> _logger;
    private readonly string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private readonly int MAX_CODE_GENERATE = 2000;
    private readonly int[] COUNT_MIN_MAX_SIZE = [7, 8];

    public DiscountService(ILogger<DiscountService> logger)
    {
        _logger = logger;

        if (!File.Exists(STORAGE_PATH))
            File.Create(STORAGE_PATH).Close();

        foreach (var line in File.ReadAllLines(STORAGE_PATH))
            UsedCodesCache.TryAdd(line, 0);
    }

    public override Task<GenerateResponse> Generate(GenerateRequest request, ServerCallContext context)
    {
        if (request.Count > MAX_CODE_GENERATE)
        {
            _logger.LogInformation($"Count parameter is out of bound. requested value:{request.Count}, expected:values between 0 and {MAX_CODE_GENERATE}");
            return Task.FromResult(new GenerateResponse { Result = false });
        }
        if (request.Length < COUNT_MIN_MAX_SIZE[0] || request.Length > COUNT_MIN_MAX_SIZE[1])
        {
            _logger.LogInformation($"Length parameter is out of bound. requested value:{request.Length}, expected:values between {COUNT_MIN_MAX_SIZE[0]} and {COUNT_MIN_MAX_SIZE[1]}");
            return Task.FromResult(new GenerateResponse { Result = false });
        }

        _logger.LogInformation($"Generating {request.Count} codes of length {request.Length}. {DateTime.Now.Ticks}");
        
        var rng = new Random();
        var newCodes = new List<string>();

        for (int i = 0; i < request.Count && newCodes.Count < MAX_CODE_GENERATE; i++)
        {
            string code;
            do
            {
                code = new string(Enumerable.Repeat(CHARS, (int)request.Length).Select(s => s[rng.Next(s.Length)]).ToArray());
            } while (UsedCodesCache.ContainsKey(code) || newCodes.Contains(code));

            newCodes.Add(code);
        }

        foreach (var code in newCodes)
        {
            //TODO add logic to create other code 
            if (!UsedCodesCache.TryAdd(code, 0))
            {
                _logger.LogInformation($"The code is already picked: {code}.");
            }
        }

        Task.Run(() => SaveToDiskAsync());

        return Task.FromResult(new GenerateResponse { Result = true });
    }

    public override Task<UseCodeResponse> UseCode(UseCodeRequest request, ServerCallContext context)
    {
        var result = UsedCodesCache.TryRemove(request.Code, out _);
        if (result)
            Task.Run(() => SaveToDiskAsync());

        return Task.FromResult(new UseCodeResponse { Result = result });
    }

    private void SaveToDiskAsync()
    {
        using (var mutex = new Mutex(false, STORAGE_PATH))
        {
            var hasHandle = false;
            try
            {
                hasHandle = mutex.WaitOne(Timeout.Infinite, false);

                File.WriteAllLines(STORAGE_PATH, UsedCodesCache.Keys);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error to Write file:{ex.Message}");
            }
            finally
            {
                if (hasHandle)
                    mutex.ReleaseMutex();
            }
        }
    }
}
