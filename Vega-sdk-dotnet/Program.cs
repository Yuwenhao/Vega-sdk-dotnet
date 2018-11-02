using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega_sdk_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://172.22.34.203/v3/ems-cartype-service/v3/ems/adapter";
            string secretKey = "YVwHC5QCpbn0wrfrnYwmChszqe2YjMmD";
            string http_verb = "GET";
            string CanonicalizedResource = "/v3/ems-cartype-service/v3/ems/adapter";
            long timestamp = Vega_sdk_dotnet_util.Util.ConvertDateTimeToInt(DateTime.Now);
            string stringtosign = http_verb + "\n" + timestamp + "\n" + CanonicalizedResource;
            string sign = Vega_sdk_dotnet_util.Util.ToBase64hmac(stringtosign, secretKey);
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("g7timestamp", timestamp.ToString());
            param.Add("sign", sign);
            param.Add("accessid", "wudte8s");
            string result = Vega_sdk_dotnet_util.Util.sendGet(uri, param);
        }
    }
}
