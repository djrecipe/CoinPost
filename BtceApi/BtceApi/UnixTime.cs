#region Using
using System;
#endregion

namespace BtcE
{
    public static class UnixTime
    {
        #region Static Members
        private static DateTime unixEpoch = new DateTime(1970, 1, 1);
        #endregion
        #region Static Properties
        public static uint Now
        {
            get
            {
                return GetFromDateTime(DateTime.UtcNow);
            }
        }
        #endregion
        #region Static Methods
        public static uint GetFromDateTime(DateTime d)
        {
            return (uint)(d - unixEpoch).TotalSeconds;
        }
        public static DateTime ConvertToDateTime(uint unixtime)
        {
            return unixEpoch.AddSeconds(unixtime);
        }
        #endregion
    }
}
