using System.ComponentModel;

namespace CommonInterfaces.Models.Authentication;

public enum HashAlgorithmType
{
    [Description("SHA256")] Sha256,
    [Description("BCRYPT")] Bcrypt
}