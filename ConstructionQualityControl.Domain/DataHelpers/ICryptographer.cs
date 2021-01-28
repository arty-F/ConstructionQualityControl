namespace ConstructionQualityControl.Domain
{
    /// <summary>
    /// Provides a mechanism to data encryption/decryption.
    /// </summary>
    public interface ICryptographer
    {
        /// <summary>
        /// Return encrypted by aes alghoritm string with IV.
        /// </summary>
        /// <param name="data">String for encryption.</param>
        string Encypt(string data);
        /// <summary>
        /// Return decrypted by aes alghoritm string.
        /// </summary>
        /// <param name="data">Ecrypted string with IV.</param>
        string Decrypt(string data);
        /// <summary>
        /// Change key for aes alghoritm and makes it suitable length.
        /// </summary>
        /// <param name="newKey">New key.</param>
        void ChangeKey(string newKey);
    }
}
