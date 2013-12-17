CoinPost
========
brkfstmnchr@gmail.com
Cryptocurrency trading app which currently only supports btc-e.
USE AT YOUR OWN RISK!

==========================================================================
INSTRUCTIONS

* Download and install Microsoft Visual Studio Express 2013 from http://www.visualstudio.com/en-us/downloads.
* Download this git directory and open CoinPost.sln.
* Create a BTC-E API key at https://btc-e.com/profile#api_keys.
* Compile and Run the program and enter your API key and secret.
* 
==========================================================================
NOTES

* Once you close your web browser your API key and secret will be hidden FOREVER. If you lose the CoinPost.key file, you will have to disable your old API key and generate a new one (which is a painless process, thankfully).
* You will have to generate a new API key and secret and a new CoinPost.key file for every computer you run the application on.
* 
==========================================================================
12/16/13 KNOWN BUGS/LIMITATIONS THAT I AM WORKING ON:

* There are several exchanges currently present on www.btc-e.com
  that do not have a corresponding www.bitcoinwisdom.com graph. (i.e. BTC/NVC)
* There are certain ways in which it is possible to try and make invalid orders. This will result in an endless series   of message boxes and you might have to CTRL+ALT+DEL. The primary way this bug is invoked is by trying to commit an     order with too many decimal numbers (via order modification).
* 
==========================================================================
