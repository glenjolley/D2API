using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace D2API.Services;

public class D2Service : ID2Service
{
    private readonly IConfiguration _configuration;
    private string _fileName;
    private string _arguments;

    public D2Service(IConfiguration configuration)
    {
        _configuration = configuration;
        _fileName = _configuration["D2Settings:Filename"] ?? "d2";
        _arguments = _configuration["D2Settings:Arguments"] ?? "- -";
    }

    public string CreateSVG(string input)
    {
        try
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = _fileName,
                    Arguments = _arguments,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };

            process.Start();
            StreamWriter sw = process.StandardInput;

            sw.WriteLine(input);
            sw.Close();

            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
