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
T{ .( two words are adjacent if they are the same size and differ by only one letter ) CR
    S" DOG" S" HORSE" ADJACENT? FALSE ?S
    S" DOG" S" FOG" ADJACENT? TRUE ?S
    S" DOG" S" DOG" ADJACENT? FALSE ?S
}T
T{ .( after creation ar is empty ) CR
    1000 AR-CREATE my-array
    my-array AR-EMPTY? TRUE ?S
}T
T{ .( after adding a value, array has one more value ) CR
    4807 my-array AR-ADD
    23 my-array AR-ADD
    17 my-array AR-ADD
    my-array CELL+ @ 3 ?S
}T
T{ .( after adding a value in array, value can be found ) CR
    4807 my-array AR-EXIST? TRUE ?S
    3256 my-array AR-EXIST? FALSE ?S
}T
T{ .( after emptying array, value cannot be found ) CR
    17 my-array AR-EXIST? TRUE ?S
    my-array AR-EMPTY
    17 my-array AR-EXIST? FALSE ?S
}T
T{ .( after finding neighbors, array contains neigbors ) CR
    S" dog" my-list my-array WL-NEIGHBORS
    my-array AR-SIZE 11 ?S
    S" evil" my-list my-array WL-NEIGHBORS
    my-array AR-SIZE 0 ?S
}T
T{ .( after creation associative array is empty ) CR
    256 AA-CREATE my-assoc
    my-assoc AA-EMPTY? TRUE ?S
}T
T{ .( after adding or update a key/value value can be found ) CR
    2317 4807 my-assoc AA-ADD-UPDATE
    4807 my-assoc AA-FIND TRUE ?S 2317 ?S
}T
T{ .( after emptying assoc array values cannot be found ) CR
    my-assoc AA-EMPTY
    4807 my-assoc AA-FIND FALSE ?S
}T
T{ .( after updating an assoc array key value value is repladed ) CR
    2317 4807 my-assoc AA-ADD-UPDATE
    4807 my-assoc AA-FIND TRUE ?S 2317 ?S
    1723 4807 my-assoc AA-ADD-UPDATE
    4807 my-assoc AA-FIND TRUE ?S 1723 ?S
}T

BYE
