using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zeeyo.Service.Exceptions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.DTOs.SmsMessages;
using Zeeyo.Service.Interfaces.Accounts;
using Microsoft.Extensions.Configuration;

namespace Zeeyo.Service.Services.Accounts;

public class SmsService : ISmsService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<User> _userRepository;

    public SmsService(IConfiguration configuration, IRepository<User> userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<string> GenerateTokenAsync()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "https://notify.eskiz.uz/api/auth/login");
        var content = new MultipartFormDataContent();
        content.Add(new StringContent($"{_configuration["SmsConfig:Email"]}"), "email"); // configuration["TelegramBotConfig:BotToken"];
        content.Add(new StringContent($"{_configuration["SmsConfig:Password"]}"), "password");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();   // Check for whether send or not

        var token = await response.Content.ReadAsStringAsync();

        var jsonToken = JsonConvert.DeserializeObject<JObject>(token);

        var tokenGenereted = jsonToken["data"]["token"].ToString();

        return tokenGenereted;
    }


    public async Task<bool> SendAsync(Message message)
    {
        var user = await _userRepository.SelectAsync(u => u.Id == message.UserId);


        if (user is null)
            throw new ZeeyoException(404, "User is not found");

        var token = await GenerateTokenAsync();
        
        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, "https://notify.eskiz.uz/api/message/sms/send");

        // Add the Authorization header with the Bearer token
        request.Headers.Add("Authorization", $"Bearer {token}");

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent($"{user.PhoneNumber}"), "mobile_phone");
        content.Add(new StringContent($"{message.Data} \n {message.Url}"), "message");
        content.Add(new StringContent($"{_configuration["SmsConfig:from"]}"), "from");
        request.Content = content;
        await client.SendAsync(request);
          
        return true;
    }
}
