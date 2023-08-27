\ test-wordkey.fs
REQUIRE ../src/wordkey.fs
CR
.( word keys ) CR

T{ .(   a string of at most 7 chars can get stored on a single cell as a key. ) CR
    s" tractor" S>KEY s" traitor" S>KEY = ?FALSE
    s" blink" S>KEY s" blink" S>KEY ?S
   .(   if the string is too long, a an exception occurs. ) CR
: recover true ;
: check-string-too-large-exception
    s" abracadabra" ['] recover catch S>KEY ;
    check-string-too-large-exception drop ?TRUE
}T

T{ .(   a string as key can be given from input flow.) CR
    KEY" dog" s" dog" S>KEY ?S
}T
T{ .(   a S>KEY cell can be converted back into a string on the pad. ) CR
    KEY" slope" pad KEY>S s" slope" ?STR
}T

