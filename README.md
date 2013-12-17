CoinPost
========
* brkfstmnchr@gmail.com
* Cryptocurrency trading app which currently only supports btc-e.
* USE AT YOUR OWN RISK!
* Currently this program is in its infancy. Although it is somewhat operational, most of its functionality is simply proving as a proof-of-concept. I plan on pouring a lot of time and effort into this project, so please send me any feedback and suggestions you have. My primary goal is to make a fast, effecient, painless, -and eventually- automated interface for trading cryptocurrencies.

INSTRUCTIONS
==========================================================================
* Download and install Microsoft Visual Studio Express 2013 from http://www.visualstudio.com/en-us/downloads.
* Download this git directory and open CoinPost.sln.
* Create a BTC-E API key at https://btc-e.com/profile#api_keys.
* Compile and run the program and enter your API key and secret.

USAGE
==========================================================================
* Change the two drop-down lists (in the lower bottom-left corner) to reflect the two currencies you wish to trade. A bitcoinwisdom chart will be displayed if one exists for that currency.
* Try clicking the 'Max' buttons to see what they do. Also, you can try clicking your balance for any given currency and your price/quantity fields will be intelligently updated depending on your currently selected currencies.
* Cancel or modify an already existing order by clicking on the "X" (cancel) or "M" (modify) buttons next to the order.


NOTES
==========================================================================
* Once you close your web browser your API key and secret will be hidden FOREVER. If you lose the CoinPost.key file, you will have to disable your old API key and generate a new one (which is a painless process, thankfully).
* You will have to generate a new API key and secret and a new CoinPost.key file for every computer you run the application on.

KNOWN BUGS/LIMITATIONS
==========================================================================
12/16/13:
* There are several exchanges currently present on www.btc-e.com that do not have a corresponding www.bitcoinwisdom.com graph. (i.e. BTC/NVC) I will be handling these cases and directing to a different website.
* There are certain ways in which it is possible to try and make invalid orders. This will result in an endless series of message boxes and you might have to CTRL+ALT+DEL. The primary way this bug is invoked is by trying to commit an order with too many decimal numbers (via order modification).

==========================================================================
