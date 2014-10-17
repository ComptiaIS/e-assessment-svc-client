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

            // Create person
            var sourceId = Guid.NewGuid();
            var hdr = new imsx_RequestHeaderInfoType();
            var req = new createPersonRequest();
            req.personRecord = UserMapper.MapToRecord(new User { SourceId = sourceId.ToString(), FirstName = "Vlad", LastName = "Ogay", Language = "en" });
            client.createPerson(hdr, req);

            // Read person that we've just created
            var req2 = new readPersonRequest {sourcedId = sourceId.ToString()};
            readPersonResponse resp2;
            client.readPerson(hdr, req2, out resp2);
        }
    }
}
