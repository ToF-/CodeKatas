\ tests.fs
REQUIRE ffl/tst.fs
REQUIRE word-ladder.fs
PAGE

T{ .( after creation wl is empty ) CR
    256 WL-CREATE my-list
    my-list WL-EMPTY? TRUE ?S
}T
T{ .( when empty wl cannot find a word ) CR
    S" DOG" my-list WL-FIND FALSE ?S 
}T
T{ .( after adding a word, wl can find the word ) CR
    S" DOG" my-list  WL-ADD 
    S" DOG" my-list WL-FIND
    COUNT S" DOG" ?STR
}T
BYE 
T{ .( clear dictionnary, not finding any word ) CR
    S" DOG" my-list WL-FIND FALSE ?S
}T
BYE
T{ .( adding words, finding the words exist ) CR
    MAIN WORD-LIST-CLEAR
    S" DOG" MAIN dbg ADD-WORD-L
    S" FOG" MAIN ADD-WORD-L
    S" COG" MAIN ADD-WORD-L
    S" DOG" MAIN FIND-WORD-L TRUE ?S COUNT S" DOG" ?STR
    S" FOO" MAIN FIND-WORD-L FALSE ?S
    S" COG" MAIN FIND-WORD-L TRUE ?S COUNT S" COG" ?STR
}T
BYE
T{ .( adding words from a file ) CR
    CLEAR-DICTIONARY
    S" ../../src/3-letter-words.txt" READ-WORDS
    S" ../../src/4-letter-words.txt" READ-WORDS
    S" ../../src/5-letter-words.txt" READ-WORDS
    S" about" WORD-EXIST? TRUE ?S
    S" zooms" WORD-EXIST? TRUE ?S
    S" fubar" WORD-EXIST? FALSE ?S
}T
T{ .( two words are adjacent if they are the same size and differ by only one letter ) CR
    S" DOG" S" HORSE" ADJACENT? FALSE ?S
    S" DOG" S" FOG" ADJACENT? TRUE ?S
    S" DOG" S" DOG" ADJACENT? FALSE ?S
}T

BYE
