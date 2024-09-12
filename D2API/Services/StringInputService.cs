using System.IO.Compression;
using System.Text;

namespace D2API.Services;

public class StringInputService : IStringInputService
{
    public string DecompressString(byte[] bytes)
    {
        try
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var zs = new ZLibStream(msi, CompressionMode.Decompress))
                {
                    zs.CopyTo(mso);
                    return Encoding.UTF8.GetString(mso.ToArray());
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
