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
    s" slope" S>KEY pad KEY>S s" slope" ?STR
}T

T{ .( two S>KEYs are adjacent if their string are.) CR
    s" dog" S>KEY s" fog" S>KEY KEY-ADJACENT? ?TRUE
    s" bag" S>KEY s" fog" S>KEY KEY-ADJACENT? ?FALSE
}T

T{ .( a string as key can be given from input flow.) CR
    KEY" dog" s" dog" S>KEY ?S
}T
T{ .( words can be added in and retrieved from a word dictionary. ) CR
    WORD-DICTIONARY dict
    KEY" dog" dict FIND-WORD ?FALSE
    4807 KEY" dog" dict ADD-WORD
    KEY" dog" dict FIND-WORD ?TRUE 4807 ?S
    1000 KEY" cat" dict ADD-WORD
    4096 KEY" dog" dict ADD-WORD
    KEY" dog" dict FIND-WORD ?TRUE 4096 ?S
    dict ACT-LENGTH@ 2 ?S
}T

T{ .( words can be read from a file into a dictionary. ) CR
    s" sample.txt" dict READ-WORDS
    KEY" bee" dict FIND-WORD ?TRUE DROP
    KEY" wax" dict FIND-WORD ?TRUE DROP
}T

dict ACT-CLEAR
s" small.txt" dict READ-WORDS

T{ .( after visiting the word, the word is visited. ) CR
    KEY" eel" dict FIND-WORD ?TRUE ?FALSE
    KEY" eel" dict VISIT-WORD
    KEY" eel" dict FIND-WORD ?TRUE ?TRUE
}T

T{ .( after clearing word values all words values are set fo false. ) CR
    KEY" cat" dict VISIT-WORD
    KEY" dog" dict VISIT-WORD
    dict CLEAR-WORD-VALUES
    KEY" eel" dict FIND-WORD ?TRUE ?FALSE
    KEY" cat" dict FIND-WORD ?TRUE ?FALSE
    KEY" dog" dict FIND-WORD ?TRUE ?FALSE
}T  
BYE

