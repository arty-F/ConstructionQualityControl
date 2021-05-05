using ConstructionQualityControl.Domain;
using NUnit.Framework;

namespace ConstructionQualityControl.ModuleTests
{
    public class CryptographerTests
    {
        Cryptographer cryptographer;
        const string key = "my_secret_key123";

        [SetUp]
        public void Setup()
        {
            cryptographer = new Cryptographer(key);
        }

        [Test]
        public void Positive_Encrypted_Message_Is_Not_Same()
        {
            var expected = "message";
            var actual = cryptographer.Encypt(expected);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void Positive_Decrypted_Message_Is_Same()
        {
            var expected = "message";
            var encrypted = cryptographer.Encypt(expected);
            var actual = cryptographer.Decrypt(encrypted);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Positive_After_Key_Changing_Message_Are_Not_Same()
        {
            var expected = "message";
            var encrypted = cryptographer.Encypt(expected);
            cryptographer.ChangeKey("New key");
            var actual = cryptographer.Encypt(encrypted);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void Positive_After_Same_Key_Changing_Message_Are_Same_Too()
        {
            var expected = "message";
            var encrypted = cryptographer.Encypt(expected);
            cryptographer.ChangeKey(key);
            var actual = cryptographer.Decrypt(encrypted);

            Assert.AreEqual(expected, actual);
        }
    }
}
