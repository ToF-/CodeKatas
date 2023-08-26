# A general Approach

An interesting way of designing a solution for this problem is to try to solve it by hand on a small data set. 

Let's suppose we want to find the shortest path between the words _cat_ and _dog_.

We start with _cat_ search a dictionary for all 3 letters words that are only different from _cat_ by one letter and draw a small tree with these words.

_cat_ → _bat_, _cab_, _cad_, _cam_, _can_, _cap_, _car_, _cot_, _cut_, _eat_, _fat_, _hat_, _mat_, _oat_, _pat_, _rat_, _sat_, _vat_

then we get to these words in turn, and for each of them do a similar search, adding only the words we haven't seen yet to the tree.

_bat_ → _bad_, _bag_, _ban_, _bap_, _bar_, _bay_, _bet_, _bit_, _but_
_cab_ → _cob_, _cub_, _dab_, _jab_, _tab_
_cad_ → _cod_, _cud_, _fad_, _gad_, _had_, _lad_, _mad_, _pad_, _sad_, _wad_
_cam_ → _dam_, _ham_, _jam_, _ram_, _yam_
_can_ → _con_, _fan_, _man_, _pan_, _ran_, _tan_, _van_, _wan_
_cap_ → _bap_, _cup_, _gap_, _lap_, _map_, _nap_, _pap_, _rap_, _sap_, _tap_, _yap_
_car_ → _cur_, _ear_, _far_, _jar_, _mar_, _oar_, _par_, _tar_, _war_
_cot_ → _cog_, _col_, _con_, _cow_, _cox_, _coy_, _dot_, _got_, _hot_, _jot_, _lot_, _not_, _pot_, _rot_, _sot_, _tot_
_cut_ → _cue_, _cup_, _cur_, _gut_, _hut_, _jut_, _nut_, _out_, _put_, _rut_
_eat_ → _ear_
_fat_ → _fag_, _fax_, _fit_
_hat_ → _hag_, _ham_, _has_, _hay_, _hit_
_mat_ → _maw_, _may_, _met_
_oat_ → _oaf_, _oak_, _oar_, _oft_, _opt_, _out_
_pat_ → _pal_, _pap_, _par_, _paw_, _pay_, _pet_, _pit_
_rat_ → _rag_, _raj_, _raw_, _ray_
_sat_ → _sag_, _saw_, _say_, _set_, _sit_, _sot_
_vat_ → _van_, _vet_

Then we go again with the words corresponding to the first leaves that we added to the tree:

_bad_ → _bed_, _bid_, _bud_
_bag_ → _beg_, _big_, _bog_, _bug_, _fag_, _gag_, _hag_, _jag_, _lag_, _nag_, _rag_, _sag_, _tag_, _wag_
_ban_ → _bin_, _bun_
_bap_ → _bop_
_bay_ → _boy_, _buy_, _day_, _gay_, _lay_, _nay_, _way_
_bet_ → _bee_, _get_, _jet_, _let_, _net_, _wet_, _yet_
_bit_ → _bib_, _kit_, _lit_, _nit_, _tit_, _wit_
_but _ → _bus_, _buy_




