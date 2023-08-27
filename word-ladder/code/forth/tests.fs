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

T{ .( a string as key can be given from input flow.) CR
    KEY" dog" s" dog" S>KEY ?S
}T
T{ .( a S>KEY cell can be converted back into a string on the pad. ) CR
    KEY" slope" pad KEY>S s" slope" ?STR
}T

T{ .( two S>KEYs are adjacent if their string are.) CR
    KEY" dog" KEY" fog" KEY-ADJACENT? ?TRUE
    KEY" bag" KEY" fog" KEY-ADJACENT? ?FALSE
}T

T{ .( words can be added and updated in the dictionary. ) CR
    WORD-DICTIONARY dict
    KEY" cog" KEY" dog" dict WORD-PREDECESSOR! 
    KEY" dog" dict WORD-PREDECESSOR@ KEY" cog" ?S
    KEY" cat" dict NEW-WORD
    KEY" cat" dict WORD-PREDECESSOR@ 0 ?S
    dict ACT-LENGTH@ 2 ?S
}T

T{ .( words predecessor can have there value updated. ) CR
    KEY" flu" KEY" fly" dict WORD-PREDECESSOR!
    KEY" fly" dict FIND-WORD ?TRUE KEY" flu" ?S
}T
    
T{ .( words can be read from a file into a dictionary. ) CR
    s" sample.txt" dict READ-WORDS
    KEY" bee" dict FIND-WORD ?TRUE DROP
    KEY" wax" dict FIND-WORD ?TRUE DROP
}T

dict ACT-CLEAR
s" small.txt" dict READ-WORDS
\    dict .WORD-DICTIONARY


T{ .( after clearing word values all words values are set fo false. ) CR
    23 KEY" cat" dict WORD-PREDECESSOR!
    17 KEY" dog" dict WORD-PREDECESSOR!
    dict CLEAR-WORD-PREDECESSORS
    KEY" eel" dict FIND-WORD ?TRUE ?FALSE
    KEY" cat" dict FIND-WORD ?TRUE ?FALSE
    KEY" dog" dict FIND-WORD ?TRUE ?FALSE
}T

CREATE keys 10 CELLS ALLOT

T{ .( after searching adjacent words, the keys are stored. ) CR
    KEY" cat" dict keys FIND-ADJACENT-WORDS 2 ?S
    keys @ pad KEY>S S" cab" ?STR
    keys cell+ @ pad KEY>S S" cot" ?STR
}T

T{ .( words that were found to be adjacent are not searched again. ) CR  
    KEY" cat" dict keys FIND-ADJACENT-WORDS 0 ?S
    KEY" dab" dict keys  FIND-ADJACENT-WORDS 1 ?S
    keys @ pad KEY>S s" dam" ?STR
}T

ACT-CREATE my-path
T{ .( path go from target to origin. ) CR
   0         KEY" dog" my-path act-insert
   KEY" dog" KEY" cog" my-path act-insert
   KEY" cog" KEY" cot" my-path act-insert
   KEY" cot" KEY" cat" my-path act-insert
   .( should print cat cot cog dog : ) CR
   KEY" cat" my-path .LADDER 
}T

0 CAR-CREATE my-visit 
T{ .( after initialization visit list is empty. ) CR
    my-visit CLEAR-VISITS
    my-visit TO-VISIT? ?FALSE
}T
T{ .( after adding a node to visit list is not empty. ) CR
    4807 my-visit TO-VISIT+! 
    my-visit TO-VISIT? ?TRUE
}T
T{ .( extracting nodes to visit is first in first out. ) CR
    23 my-visit TO-VISIT+!
    17 my-visit TO-VISIT+!
    my-visit TO-VISIT@ 4807 ?S
    my-visit TO-VISIT@ 23 ?S
    my-visit TO-VISIT@ 17 ?S
    my-visit TO-VISIT? ?FALSE
}T    
BYE
