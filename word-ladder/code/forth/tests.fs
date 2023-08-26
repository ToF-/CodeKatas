\ tests.fs
REQUIRE ffl/tst.fs
REQUIRE wordladder.fs
PAGE
T{ .( two strings are adjacent if they are the same size and differ by one char only. ) CR
    s" dog" s" horse" ADJACENT? ?FALSE
    s" dog" s" fog"   ADJACENT? ?TRUE
    s" dog" s" bag"   ADJACENT? ?FALSE
    s" "    s" "      ADJACENT? ?FALSE
}T

T{ .( a string of at most 7 chars can get stored on a single cell as a key. ) CR
    s" tractor" S>KEY s" traitor" S>KEY = ?FALSE
    s" blink" S>KEY s" blink" S>KEY ?S
   .( if the string is too long, a message is sent and the cell is null. ) CR
   ." string too large expected: " s" abracadabra" S>KEY 0 ?S
}T

T{ .( a S>KEY cell can be converted back into a string on the pad. ) CR
    s" slope" S>KEY KEY>S s" slope" ?STR
}T
BYE

