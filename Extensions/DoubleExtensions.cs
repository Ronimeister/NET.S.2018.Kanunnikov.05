using System.Runtime.InteropServices;
using System.Text;

namespace Extensions
{
    /// <summary>
    /// Class that provides extension methods to System.Double
    /// </summary>
    public static class DoubleExtensions
    {
        #region Constants
        private const int BitsInByte = 8;
        private const int BitsInLong = BitsInByte * 8;
        #endregion

        #region Public API
        /// <summary>
        /// Method that provides functionality to System.Double to repsresent it in IEEE 754 Format Style.
        /// </summary>
        /// <param name="number">Value that need to be represent in IEEE 754 Format Style.</param>
        /// <returns>IEEE 754 string representation.</returns>
        public static string DoubleToBinary(this double number)
        {
            DoubleToLongStruct bits = new DoubleToLongStruct(number);
            return bits.ToLong().ConvertToIEEE();
        }
        #endregion

        #region Private API
        /// <summary>
        /// Method that convert long value into the IEEE 754 Format Style string.
        /// </summary>
        /// <param name="bits">Value that need to be represent in IEEE 754 Format Style.</param>
        /// <returns>IEEE 754 string representation.</returns>
        private static string ConvertToIEEE(this long bits)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < BitsInLong; i++)
            {
                if ((bits & 1) == 1)
                {
                    result.Insert(0, "1");
                }
                else
                {
                    result.Insert(0, "0");
                }

                bits >>= 1;
            }

            return result.ToString();
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct DoubleToLongStruct
        {
            [FieldOffset(0)]
            private readonly long _long64bits;
            
            [FieldOffset(0)]
            private readonly double _double64bits;

            public DoubleToLongStruct(double value) : this()
            {
                _double64bits = value;
            }

            public long ToLong() => _long64bits;
        }
        #endregion
    }
}