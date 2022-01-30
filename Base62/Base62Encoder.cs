using Org.BouncyCastle.Math;
using System;
using System.Linq;
using System.Text;

namespace Base62
{
    public static class Base62Encoder
    {
        private static readonly char[] _base_62_chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly BigInteger _sixty_two = BigInteger.ValueOf(value: 62L);

        public static string Decode(string base62)
        {
            var value = Base62ToBigInteger(base62);
            var str = value.ToString(16);
            var len = str.Length;
            if (len < 32)
            {
                var sb = new StringBuilder(32);
                var leadingZeroesCount = 32 - len;
                for (var i = 0; i < leadingZeroesCount; ++i)
                {
                    sb.Append('0');
                }
                sb.Append(str);
                str = sb.ToString();
            }
            return DecodingHexStringToString(str);
        }

        public static string Encode(string value)
        {
            var hex = EncodingStringToHexString(value);
            var bigIntValue = new BigInteger(hex, 16);
            var base62 = new StringBuilder();
            if (bigIntValue.SignValue < 0)
            {
                base62.Append('-');
            }
            var bal = bigIntValue.Abs();
            int digit;
            while (bal.CompareTo(_sixty_two) > 0)
            {
                digit = bal.Mod(_sixty_two).IntValue;
                base62.Append(_base_62_chars[digit]);
                bal = bal.Divide(_sixty_two);
            }
            digit = bal.Mod(_sixty_two).IntValue;
            base62.Append(_base_62_chars[digit]);
            return base62.ToString();
        }

        private static BigInteger Base62ToBigInteger(string base62)
        {
            var value = BigInteger.Zero;
            var multiplier = BigInteger.One;
            for (var i = 0; i < base62.Length; ++i)
            {
                var c = base62.ElementAt(i);
                var digit = DigitValue(c);
                value = value.Add(multiplier.Multiply(digit));
                multiplier = multiplier.Multiply(_sixty_two);
            }
            return value;
        }

        private static BigInteger DigitValue(char c)
        {
            for (var i = 0; i < _base_62_chars.Length; ++i)
            {
                if (_base_62_chars[i] == c)
                {
                    return BigInteger.ValueOf(i);
                }
            }
            throw new FormatException("Invalid base 62 character: " + c);
        }
        private static string EncodingStringToHexString(string value)
        {
            var sb = new StringBuilder();
            var bytes = Encoding.Unicode.GetBytes(value);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        private static string DecodingHexStringToString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes);
        }
    }
}
