using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using SampleServiceClient.ServiceReference1;

namespace SampleServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ServiceReference1.PersonManagerSyncPortTypeClient();
            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            client.ClientCredentials.UserName.UserName = "user1";
            client.ClientCredentials.UserName.Password = "tratata";
            var hdr = new imsx_RequestHeaderInfoType();
            var req = new createPersonRequest();

            req.personRecord = UserMapper.MapToRecord(new User { SourceId = Guid.NewGuid().ToString(), FirstName = "Vlad", LastName = "Ogay", Language = "en" });
            client.createPerson(hdr, req);
        }
    }
}
