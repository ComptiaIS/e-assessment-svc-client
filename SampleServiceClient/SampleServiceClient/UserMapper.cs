using System;
using SampleServiceClient.ServiceReference1;

namespace SampleServiceClient
{
    public class UserMapper
    {
        public static PersonRecordType MapToRecord(User user)
        {
            if (user == null) throw new ArgumentNullException();

            var result = new PersonRecordType();
            result.sourcedGUID = new SourcedGUIDType();
            result.sourcedGUID.sourcedId = user.SourceId;

            var person = new PersonType
            {
                name = new []
                {
                    new NameType
                    {
                        nameType = new BaseValueTokenType
                        {
                            instanceIdentifier = new TextType {language = "en", textString = "0"},
                            instanceVocabulary = "http://www.imsglobal.org/lis/pmsv2p0/partnamevocabularyv1p0",
                            instanceValue = new TextType {language = "en", textString = "Full"},
                        },
                        partName = new []
                        {
                            new BaseValueSingleType
                            {
                                instanceIdentifier = new TextType {language = "en", textString = "1"},
                                instanceName =
                                    new TextType {language = user.Language, textString = VocabularyConstants.FirstName},
                                instanceVocabulary = "http://www.imsglobal.org/lis/pmsv2p0/partnamevocabularyv1p0",
                                instanceValue = new TextType {language = "en", textString = user.FirstName},
                            },
                            new BaseValueSingleType
                            {
                                instanceIdentifier = new TextType {language = user.Language, textString = "2"},
                                instanceName =
                                    new TextType {language = user.Language, textString = VocabularyConstants.LastName},
                                instanceVocabulary = "http://www.imsglobal.org/lis/pmsv2p0/partnamevocabularyv1p0",
                                instanceValue = new TextType {language = "en", textString = user.LastName},
                            }
                        }
                    }
                }
            };
            result.person = person;
            return result;
        }
    }
}
