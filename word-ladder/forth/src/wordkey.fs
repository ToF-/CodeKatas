\ wordkey.fs

CREATE WORDKEY-BUFFER CELL ALLOT


: S>WORDKEY ( add,l -- w )
    WORDKEY-BUFFER OFF
    WORDKEY-BUFFER PLACE
    WORDKEY-BUFFER @ ;

: WORDKEY>S ( w,add -- )
    ! ;
    
    
