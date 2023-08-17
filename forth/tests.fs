require ffl/tst.fs
require wordladder.fs

T{
    S" aaaaa" S>5L-VALUE 0 ?S
    S" aaaab" S>5L-VALUE 1 ?S
    s" AAAAA" S>5L-VALUE 0 ?S
    s" AAAAZ" S>5L-VALUE 25 ?S
    s" AAABA" S>5L-VALUE 26 ?S
    s" AAABB" S>5L-VALUE 27 ?S
    s" AAAZZ" S>5L-VALUE 675 ?S
    s" zzzzz" S>5L-VALUE 26 26 26 26 26 * * * * 1- ?S
    s" cargo" S>5L-VALUE 925614 ?S
    s" alban" S>5L-VALUE 194025 ?S
    
    s" clara" S>5L-VALUE 5L-VALUE-CHARS 
    CHAR C ?S CHAR L ?S CHAR A ?S CHAR R ?S CHAR A ?S
    s" tests" S>5L-VALUE .5L  \ should print TESTS
}T

BYE
