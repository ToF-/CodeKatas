\ wl-dictionary-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/wl-queue.fs
REQUIRE ../src/wl-dictionary.fs

CR .( wl-dictionary ) CR

WL-DICTIONARY dct
.(   a dictionary can store and retrieve a word with no predecessor. ) CR
T{
    WW cot dct WLD-ADD-WORD
    WW cot dct WLD-PRED@ 0 ?S
    WW cot dct WLD-HAS-WORD? ?TRUE
}T

.(   a dictionary can store and retrieve the predecessor of word. ) CR
T{
    WW cat WW cot dct WLD-PRED!
    WW cot dct WLD-PRED@ WW cat ?S
}T
.(   a dictionary can set a word to be the start of a path.) CR
T{  
    WW cot dct WLD-IS-START? ?FALSE
    WW cat dct WLD-START!
    WW cat dct WLD-IS-START? ?TRUE
}T
.(   a dictionary can store the word groups of a word that was added and retrieve their letter sets. ) CR
T{
    WW cat dct WLD-UPDATE-WORD-GROUPS
    WG ~at dct WLD-LETTER-SET@ PAD LS>S PAD COUNT S" c" ?STR
    WG c~t dct WLD-LETTER-SET@ PAD LS>S PAD COUNT S" a" ?STR
    WG ca~ dct WLD-LETTER-SET@ PAD LS>S PAD COUNT S" t" ?STR
    WW cot dct WLD-UPDATE-WORD-GROUPS
    WG c~t dct WLD-LETTER-SET@ PAD LS>S PAD COUNT S" ao" ?STR
    WW cut dct WLD-UPDATE-WORD-GROUPS
    WG c~t dct WLD-LETTER-SET@ PAD LS>S PAD COUNT S" aou" ?STR
    dct .WL-DICTIONARY
}T
.(   a dictionary can find adjacent words to a word, mark their predecessor and add them to a queue. ) CR
T{
    QUEUE q
    WL-DICTIONARY d

    WW bat WW cab WW cat WW cot WW dab WW dog WW cog WW fog WW fox WW fly 
    : add-words 0 ?do dup d WLD-ADD-WORD d WLD-UPDATE-WORD-GROUPS loop ;
    10 add-words
    WW cat q d WLD-FIND-AJACENTS!
    WW cot d WLD-PRED@ WW cat ?S
}T

