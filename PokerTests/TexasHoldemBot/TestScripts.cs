using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTests.TexasHoldemBot
{
    public class TestScripts
    {
        public static StringReader GetReader(string s)
        {
            return new StringReader(s);
        }

        public const string SCRIPT1_S = @"settings player_names player0,player1
settings your_bot player0
settings timebank 2000
settings time_per_move 100
settings initial_stack 2000
settings initial_big_blind 60
settings hands_per_blind_level 10
update game round 5
update game small_blind 30
update game big_blind 60
update game on_button player1
update player0 chips 1760
update player1 chips 2240
update game bet_round preflop
update player0 hand Ah,Qh
update player1 move call
update player0 bet 60
update player1 bet 60
update player0 pot 120
update player0 amount_to_call 0
action move 2000
Output from your bot: ""call""
Engine warning: ""Invalid move: There is no bet to call.""
update game bet_round flop
update game table Jh,As,4c
update player0 bet 0
update player1 bet 0
update player0 pot 120
update player0 amount_to_call 0
action move 2000
Output from your bot: ""check""
update player1 move check
update game bet_round turn
update game table Jh,As,4c,5s
update player0 bet 0
update player1 bet 0
update player0 pot 120
update player0 amount_to_call 0
action move 2000
Output from your bot: ""check""
update player1 move check
update game bet_round river
update game table Jh,As,4c,5s,Qs
update player0 bet 0
update player1 bet 0
update player0 pot 120
update player0 amount_to_call 0
action move 2000
Output from your bot: ""fold""
update player1 wins 120";

    }
}
