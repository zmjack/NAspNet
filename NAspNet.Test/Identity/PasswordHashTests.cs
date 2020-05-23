using Microsoft.AspNetCore.Identity;
using Xunit;

namespace NAspNet.Identity.Test
{
    public class PasswordHashTests
    {
        [Fact]
        public void Test1()
        {
            // password: NAspNet
            // v2
            Assert.Equal(
                expected: PasswordVerificationResult.SuccessRehashNeeded,
                actual: PasswordHasher.VerifyHashedPassword("AED5JfeSYk/f2lfoMQ6QooYBpIL9+6X1hXm3wfvrDcaaKeHLtxEMVVxhlt4I9Zqrkg==", "NAspNet"));

            // v3
            Assert.Equal(
                expected: PasswordVerificationResult.Success,
                actual: PasswordHasher.VerifyHashedPassword("AQAAAAEAACcQAAAAEPTTPNJh0IklUeScKpTgCk6wq+nUN49qGkfgmXrXOb32aYV6nQ3900DRULd5eAnbAg==", "NAspNet"));

            // password: naspnet
            // v2 
            Assert.Equal(
                expected: PasswordVerificationResult.Failed,
                actual: PasswordHasher.VerifyHashedPassword("ALzpIGlWHC+/+WDls/iZqlGCR75roGkYpq2GdAzUqh/rKz5pHTgjOVBQDlOHzFU2wQ==", "NAspNet"));

            // v3
            Assert.Equal(
                expected: PasswordVerificationResult.Failed,
                actual: PasswordHasher.VerifyHashedPassword("AQAAAAEAACcQAAAAEIwvO1SL1Bxiy4P/8Cva0WTqwhCCjsz0E+DgI+p+2HmQYZAr2JLoCzEP911E/i3U9A==", "NAspNet"));

        }

    }
}
