\ queue.fs

REQUIRE ffl/car.fs

: QUEUE ( n <name> -- )
    0 CAR-CREATE ;

: Q-EMPTY? ( q -- f )
   CAR-LENGTH@ 0= ;

: Q-HEAD@ ( q -- n )
    0 SWAP CAR-GET ;

: Q-APPEND ( n,q -- )
    CAR-APPEND ;

: Q-POP ( q -- n )
    0 SWAP CAR-DELETE ;

: Q-EMPTY ( q -- )
    BEGIN
        DUP Q-EMPTY? 0= WHILE
        DUP Q-POP DROP
    REPEAT DROP ;


