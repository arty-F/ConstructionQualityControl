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
        public void Encrypted_message_is_NOT_same()
        {
            var expected = "message";
            var actual = cryptographer.Encypt(expected);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void Decrypted_message_is_same()
        {
            var expected = "message";
            var encrypted = cryptographer.Encypt(expected);
            var actual = cryptographer.Decrypt(encrypted);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void After_key_changing_message_are_not_same()
        {
            var expected = "message";
            var encrypted = cryptographer.Encypt(expected);
            cryptographer.ChangeKey("New key");
            var actual = cryptographer.Encypt(encrypted);

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void After_same_key_changing_message_are_same_too()
        {
            var expected = "message";
            var encrypted = cryptographer.Encypt(expected);
            cryptographer.ChangeKey(key);
            var actual = cryptographer.Decrypt(encrypted);

            Assert.AreEqual(expected, actual);
        }
    }
}
