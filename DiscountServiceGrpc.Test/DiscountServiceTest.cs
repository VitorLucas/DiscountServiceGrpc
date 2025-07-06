using DiscountServiceGrpc.Services;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace DiscountServiceGrpc.Test;

public class DiscountServiceTest
{
    private readonly DiscountService _discountService;
    public DiscountServiceTest()
    {
        _discountService = new DiscountService(new Mock<ILogger<DiscountService>>().Object);
    }


    [Fact]
    public async void Generate_CountOutOfBound_ReturnFalse()
    {
        //Arrange
        var request = new GenerateRequest
        {
            Count = 2001,
            Length = 10
        };

        // Act
        var response = await _discountService.Generate(request,new Mock<ServerCallContext>().Object);
        
        //Assert
        Assert.True(response.Result == false, "Expected result to be false when count is out of bounds.");
    }

    [Fact]
    public async void Generate_LengthOutOfBiggerThanMaxSize_ReturnFalse()
    {
        //Arrange
        var request = new GenerateRequest
        {
            Count = 2000, // Invalid count
            Length = 10 // Valid length
        };

        // Act
        var response = await _discountService.Generate(request, new Mock<ServerCallContext>().Object);

        //Assert
        Assert.True(response.Result == false, "Expected result to be false when length is out of bounds.");
    }

    [Fact]
    public async void Generate_LengthOutOfLowerThanMaxSize_ReturnFalse()
    {
        //Arrange
        var request = new GenerateRequest
        {
            Count = 2000, // Invalid count
            Length = 6 // Valid length
        };

        // Act
        var response = await _discountService.Generate(request, new Mock<ServerCallContext>().Object);

        //Assert
        Assert.True(response.Result == false, "Expected result to be false when length is out of bounds.");
    }
}