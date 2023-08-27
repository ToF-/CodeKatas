\ tests.fs
REQUIRE ffl/tst.fs

REQUIRE wordladder.fs
PAGE



BYE
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

T{ .( given a word to visit, the adjacents words are updated with their predecessors. ) CR
    my-visit CLEAR-VISITS
    dict CLEAR-WORD-PREDECESSORS 
    KEY" dog" dict NEW-WORD
    KEY" dog" my-visit TO-VISIT+!
    KEY" cat" my-visit dict LADDER-STEP
    my-visit TO-VISIT@ KEY" cog" ?S 
    my-visit TO-VISIT@ KEY" dot" ?S 
    KEY" dog" dict WORD-PREDECESSOR@ 0 ?S
    KEY" cog" dict WORD-PREDECESSOR@ KEY" dog" ?S
    KEY" dot" dict WORD-PREDECESSOR@ KEY" dog" ?S
}T
    
BYE
