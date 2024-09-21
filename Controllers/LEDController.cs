using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArduinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LEDController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _esp8266Ip = "http://192.168.249.10/"; // Replace with ESP8266's IP address

        public LEDController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("on")]
        public async Task<IActionResult> TurnOnLED()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_esp8266Ip}turnOn");
                if (response.IsSuccessStatusCode)
                {
                    return Ok("LED is turned ON.");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return StatusCode(500, $"Failed to turn on LED: {errorMessage}");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("off")]
        public async Task<IActionResult> TurnOffLED()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_esp8266Ip}turnOff");
                if (response.IsSuccessStatusCode)
                {
                    return Ok("LED is turned OFF.");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return StatusCode(500, $"Failed to turn off LED: {errorMessage}");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
