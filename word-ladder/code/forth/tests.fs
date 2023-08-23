\ tests.fs
REQUIRE ffl/tst.fs
REQUIRE word-ladder.fs

T{ .( clear dictionnary, not finding any word ) CR
    S" DOG" WORD-EXIST? FALSE ?S
}T
T{ .( adding words, finding the words exist ) CR
    CLEAR-DICTIONARY
    S" DOG" ADD-WORD
    S" FOG" ADD-WORD
    S" COG" ADD-WORD
    S" DOG" WORD-EXIST? TRUE ?S
    S" FOO" WORD-EXIST? FALSE ?S
    S" COG" WORD-EXIST? TRUE ?S
}T
T{ .( adding words from a file ) CR
    CLEAR-DICTIONARY
    S" ../../src/3-letter-words.txt" READ-WORDS
    S" ../../src/4-letter-words.txt" READ-WORDS
    S" ../../src/5-letter-words.txt" READ-WORDS
    S" ABOUT" WORD-EXIST? TRUE ?S
    S" ZOOMS" WORD-EXIST? TRUE ?S
    S" FUBAR" WORD-EXIST? FALSE ?S
}T
BYE
