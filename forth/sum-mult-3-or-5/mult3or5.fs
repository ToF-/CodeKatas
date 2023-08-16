\ If we list all the natural numbers
\ below 10 that are multiples of 3 or 5,
\ we get 3, 5, 6 and 9. 
\ The sum of these multiples is 23.
\ Write a program that returns the sum 
\ of all the multiples of 3 or 5 below
\ the number passed in.
\ If the number is negative, return 0.
\ Note: if the number is a multiple of
\ both 3 and 5, only count it once.

require ffl/tst.fs

: SUM  ( n -- s )
    DUP 1+ * 2/ ;

: NB-MULTIPLES ( m,max -- n )
    1- SWAP / ;

: SUM-MULTIPLES ( max, m -- s )
    DUP ROT NB-MULTIPLES SUM * ;

: SUM-3OR5-M ( max -- n )
    DUP 0 > IF
        DUP   3 SUM-MULTIPLES
        OVER  5 SUM-MULTIPLES +
        SWAP 15 SUM-MULTIPLES -
    ELSE
        DROP 0
    THEN ;

T{
    10 SUM-3OR5-M        23 ?S
    11 SUM-3OR5-M        33 ?S
    33 SUM-3OR5-M       225 ?S
     6 SUM-3OR5-M         8 ?S
   123 SUM-3OR5-M      3420 ?S
    50 SUM-3OR5-M       543 ?S
     0 SUM-3OR5-M         0 ?S
  -203 SUM-3OR5-M         0 ?S
 10500 SUM-3OR5-M  25719750 ?S
 32768 SUM-3OR5-M 250532114 ?S
}T
BYE
