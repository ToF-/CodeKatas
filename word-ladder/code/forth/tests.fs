\ tests.fs
REQUIRE ffl/tst.fs
REQUIRE word-ladder.fs

T{ .( clear dictionnary, not finding any word ) CR
    CLEAR-DICTIONARY
    S" DOG" WORD-EXIST? 0 ?S
}T
T{ .( adding words, finding the words exist ) CR
    CLEAR-DICTIONARY
    S" DOG" ADD-WORD
    S" FOG" ADD-WORD
    S" COG" ADD-WORD
    S" DOG" WORD-EXIST? 1 ?S
    S" FO0" WORD-EXIST? 0 ?S
    S" COG" WORD-EXIST? 1 ?S
}T
T{ .( adding words from a file ) CR
    CLEAR-DICTIONARY
    S" ../../src/3-letter-words.txt" READ-WORDS
    S" ../../src/4-letter-words.txt" READ-WORDS
    S" ../../src/5-letter-words.txt" READ-WORDS
    S" ABOUT" WORD-EXIST? 1 ?S
    S" ZOOMS" WORD-EXIST? 1 ?S
    S" FUBAR" WORD-EXIST? 0 ?S
}T
BYE
T{
    .( adding a word, finding no adjacent words to that word ) CR
    CLEAR-DICTIONARY
    S" DOG" ADD-WORD
    S" DOG" FIND-ADJACENT-WORDS 0 ?S
}T

T{
    .( adding two adjacent words, finding them ) CR
    CLEAR-DICTIONARY
    S" DOG" ADD-WORD
    S" FOG" ADD-WORD
    WORD-BUFFER 16 DUMP CR
    S" DOG" FIND-ADJACENT-WORDS 1 ?S S" FOG" ?STR
    S" FOG" FIND-ADJACENT-WORDS 1 ?S S" DOG" ?STR

BYE
