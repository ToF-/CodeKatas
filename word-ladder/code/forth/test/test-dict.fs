\ test-dict.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/dict.fs

CR
.( dict ) CR

T{ .(   after updating a key and value, the value can be found in the dict. ) CR
    DICT d
    2317 4807 d D-UPDATE
    4807 d D-VALUE 2317 ?S
   .(   if the key is not in the dict, a an exception occurs. ) CR
: check-non-existing-key 999 d D-VALUE ;
    ' check-non-existing-key catch [if] TRUE [then] ?TRUE
}T

T{ .(   after clearing all values, the values in the dict are all zero. ) CR
    23 1 d D-UPDATE
    17 2 d D-UPDATE
    d D-CLEAR-VALUES
    1 d D-VALUE 0 ?S
    2 d D-VALUE 0 ?S
    4807 d D-VALUE 0 ?S
}T

