\ tests.fs
REQUIRE ffl/tst.fs
REQUIRE word-ladder.fs
PAGE

T{ .( after creation wl is empty ) CR
    100000 WL-CREATE my-list
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
T{ .( after emptying wl cannot find any word ) CR
    my-list WL-EMPTY
    S" DOG" my-list WL-FIND FALSE ?S
}T
T{ .( after reading words from a file words can be found ) CR
    my-list WL-EMPTY
    S" ../../src/3-letter-words.txt" my-list WL-READ-FILE
    S" ../../src/4-letter-words.txt" my-list WL-READ-FILE
    S" ../../src/5-letter-words.txt" my-list WL-READ-FILE
    S" fog" my-list WL-FIND 0= FALSE ?S
    S" zooms" my-list WL-FIND 0= FALSE ?S 
    S" fubar" my-list WL-FIND FALSE ?S
}T
BYE
T{ .( two words are adjacent if they are the same size and differ by only one letter ) CR
    S" DOG" S" HORSE" ADJACENT? FALSE ?S
    S" DOG" S" FOG" ADJACENT? TRUE ?S
    S" DOG" S" DOG" ADJACENT? FALSE ?S
}T

BYE
