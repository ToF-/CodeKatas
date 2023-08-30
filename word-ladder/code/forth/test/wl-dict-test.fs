\ wl-dict-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-dict.fs

CR .( wl-dict ) CR

.(   after updating a key and value, the value can be found in the dict. ) CR
T{ 
    WL-DICT d
    2317 4807 d WLD-UPDATE
    4807 d WLD-VALUE 2317 ?S
}T

.(   if the key is not in the dict, a an exception occurs. ) CR
T{
: check-non-existing-key 999 d WLD-VALUE ;

    ' check-non-existing-key catch [if] TRUE [then] ?TRUE
}T

.(   if the key is not in the dict, a zero value can be returned. ) CR
T{     
    123456 d WLD-VALUE-OR-NIL 0 ?S
}T

.(   after clearing all values, the values in the dict are all zero. ) CR
T{ 
    23 1 d WLD-UPDATE
    17 2 d WLD-UPDATE
    d WLD-CLEAR-VALUES
    1 d WLD-VALUE 0 ?S
    2 d WLD-VALUE 0 ?S
    4807 d WLD-VALUE 0 ?S
}T

