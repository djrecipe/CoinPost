using System;

namespace BtcE
{
	public enum BtcePair
	{
		btc_usd,
		btc_rur,
		btc_eur,
		ltc_btc,
		ltc_usd,
		ltc_rur,
        ltc_eur,
		nmc_btc,
        nmc_usd,
		nvc_btc,
        nvc_usd,
		usd_rur,
		eur_usd,
		trc_btc,
		ppc_btc,
        ppc_usd,
        ftc_btc,
		xpm_btc,
        Unknown
	}

	public class BtcePairHelper
	{
		public static BtcePair FromString(string s) {
			BtcePair ret = BtcePair.Unknown;
			Enum.TryParse<BtcePair>(s.ToLowerInvariant(), out ret);
			return ret;
		}
		public static string ToString(BtcePair v) {
			return Enum.GetName(typeof(BtcePair), v).ToLowerInvariant();
		}
	}
}
