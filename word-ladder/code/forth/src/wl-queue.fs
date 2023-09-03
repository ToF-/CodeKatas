\ wl-queue.fs
REQUIRE ffl/car.fs

\ create a new named queue
: QUEUE ( n <name> -- )
    0 CAR-CREATE ;

\ is the queue empty ?
: Q-EMPTY? ( q -- f )
   CAR-LENGTH@ 0= ;

\ non destructive access to 1st item
: Q-HEAD@ ( q -- n )
    0 SWAP CAR-GET ;

\ append an item at the end
: Q-APPEND ( n,q -- )
    CAR-APPEND ;

\ remove the first element
: Q-POP ( q -- n )
    0 SWAP CAR-DELETE ;

\ empty the queue
: Q-EMPTY ( q -- )
    BEGIN
        DUP Q-EMPTY? 0= WHILE
        DUP Q-POP DROP
    REPEAT DROP ;

