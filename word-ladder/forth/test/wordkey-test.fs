\ wordkey-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/wordkey.fs

CR .( wordkey ) CR
.(   a string of length 5 can be stored as a unique cell value, called its wordkey. ) CR
T{
    s" horse" S>WORDKEY
    s" again" S>WORDKEY
    SWAP PAD WORDKEY>S PAD COUNT S" horse" ?STR
    PAD WORDKEY>S PAD COUNT S" again" ?STR
}T
