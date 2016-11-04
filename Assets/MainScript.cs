using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainScript : MonoBehaviour {

	#region variable declarations
	// Variable for communicating with UI text fields
	public Text text;
	public Text name;
	public Text goldtext;
	public Text response;
	public Text portrait;
	public Text intro;
	public Text splash;
	public Text title;
	public Text goldlabel;
	public Text horizontalline;
	public Text verticalline;
	int gold;
	string goldstring;
	
	// For keeping track of progression in common room
	int commonRoomRounds;
	int drinkPurchase;
	int adviceTaken;
	
	// For handling whether a character's name is known (1) or unknown (0)
	string innkeepName;
	string chrisName;
	string gantName;
	string danielaName;
	string drakeName;
	string jessName;
	string theoName;
	
	// For describing if a character has been available (0), unavailable (1), or recruited (2)
	int chrisRecruit;
	int gantRecruit;
	int danielaRecruit;
	int drakeRecruit;
	int jessRecruit;
	int theoRecruit;
	
	// Special variables for handling interactions with characters
	int chrisAttitude;
	int chrisGender;
	int chrisLead;
	string chrisHeShe;
	int gantKnown;
    bool attemptSleep;
    bool attemptAttack;
    bool danielaSecondQuestion;
	
	// Debug display string
	string debugDisplay;
	
	// State enums to describe current game state
	private enum States {intro1, intro2, intro3, edgeton, innExamine, innkeepGreet, innkeepWho, roomRent, innkeepRefuse, 
		innkeepRefer, chrisFirst, manOrWoman, chrisMonsterSight, justWondering, clearlyNot, chrisKnockout, chrisNothing, 
		common1, common2_1, common3, common4, common5, common6, bartenderStart, bartenderAdvice1, drinkPurchase, chrisStart, 
		chrisStory, chrisMuscle, chrisBefriend, chrisEnd, gantStart, gantWhoIs1, gantWhoIs2, gantNotDrunk, gantTooDrunk, gantEnd, 
		danielaStart, danielaHelpful, danielaWhere, danielaEnd, drakeStart, drakeEating, drakeDeal, drakeCoerce, drakeEnd, jessStart, 
		jessYouOk, jessMission, bowRetrieve1, bowRetrieve2, bowDetails, bowEnd, jessCrazy, jessEnd, brotherStart, brotherEnd, 
		brotherThreat, brotherWolfman, brotherBartender, theoMutter, theoDistract1, theoDistract2, goToBed, common2_2, bartenderAdvice2, bartenderUnsure,
		bartenderRecommend, bartenderSighting, chrisBrambler, brotherSword, brotherInThere};
	private States CurrentState;
	#endregion
	
	// Use this for initialization
	void Start () {

		// Not in common room yet
		commonRoomRounds = 0;
		
		// Possible values are 0 (no drink purchased) or 1 (drink purchased)
		drinkPurchase = 0;
		
		// 0 = no advice from bartender
		adviceTaken = 0;
		
		// All characters unknown at game start
		innkeepName = "?????????";
		chrisName = "the axe man";
		gantName = "the knight";
		danielaName = "the young woman";
		drakeName = "the quiet man";
		jessName = "the archer";
		theoName = "the reclusive man";
		
		// All characters unrecruited at game start
		chrisRecruit = 0;
		gantRecruit = 0;
		danielaRecruit = 0;
		drakeRecruit = 0;
		jessRecruit = 0;
		theoRecruit = 0;
		
		// Chris starts off neutral (0) can be befriended (1) or offended (2)
		chrisAttitude = 0;
		
		// Assume Chris is male until discovered otherwise
		chrisLead = 0;
		chrisGender = 0;
		gantKnown = 0;
		chrisHeShe = "he";
        gantKnown = 0;

        // You haven't done anything yet
        attemptSleep = false;
        attemptAttack = false;
        danielaSecondQuestion = false;

        // Clear UI at game start
        text.text = "";
		name.text = "";
		response.text = "";
		portrait.text = "";
		title.text = "";
		goldlabel.text = "";
		splash.text = "";
		gold = 0;
		
		
		CurrentState = States.intro1;
	}
	
	// Update is called once per frame
	void Update () {
		// Hide the gold amount in UI until player has some
		if (gold == 0) {goldtext.text = "";}
		else {
			goldstring = gold.ToString ();
			goldtext.text = goldstring;
		}
		
		// Display Debug info
		string commonRoomRoundsSTRING = commonRoomRounds.ToString();
		string chrisAttitudeSTRING = chrisAttitude.ToString();
		string chrisRecruitSTRING = chrisRecruit.ToString();
		string drinkPurchasedSTRING = drinkPurchase.ToString();
		string currentStateSTRING = CurrentState.ToString();
		debugDisplay = "Recruitment Rounds: " + commonRoomRoundsSTRING + "\n" +
						"Chris Name: " + chrisName + "\n" +
						"Chris Attitude: " + chrisAttitudeSTRING + "\n" +
						"Chris Recruited: " + chrisRecruitSTRING + "\n" +
						"Drink Purchased: " + drinkPurchasedSTRING + "\n" +
						"Innkeep Name: " + innkeepName + "\n" +
						"Jess Name: " + jessName + "\n" +
						"Daniela Name: " + danielaName + "\n" +
						"Gant Name: " + gantName + "\n" +
						"Drake Name: " + drakeName + "\n" +
						"Theo Name: " + theoName + "\n" +
						"Current state: " + currentStateSTRING + "\n";
		portrait.text = debugDisplay;		
		
		// State List
		#region State List
		if 		(CurrentState == States.intro1)  				{Intro_1 ();}
		else if (CurrentState == States.intro2)  				{Intro_2 ();}
		else if (CurrentState == States.intro3)  				{Intro_3 ();}
		// Jolly and Chris sequence
		else if (CurrentState == States.edgeton)  				{Edgeton ();}
		else if (CurrentState == States.innExamine)  			{InnExamine ();}
		else if (CurrentState == States.innkeepGreet)  			{InnKeepGreet ();}
		else if (CurrentState == States.innkeepWho)  			{InnKeepWho ();}
		else if (CurrentState == States.roomRent)  				{RoomRent ();}
		else if (CurrentState == States.innkeepRefuse)  		{InnKeepRefuse ();}
		else if (CurrentState == States.innkeepRefer)  			{InnKeepRefer ();}
		else if (CurrentState == States.chrisFirst)  			{ChrisFirst ();}
		else if (CurrentState == States.manOrWoman)  			{ManOrWoman ();}
		else if (CurrentState == States.justWondering)  		{JustWondering ();}
		else if (CurrentState == States.clearlyNot)  			{ClearlyNot ();}
		else if (CurrentState == States.chrisMonsterSight)  	{ChrisMonsterSight ();}
		else if (CurrentState == States.chrisNothing)  			{ChrisNothing ();}
		else if (CurrentState == States.chrisKnockout)  		{ChrisKnockout ();}
		// All common room choice hubs
		else if (CurrentState == States.common1)  				{CommonRoom1 ();}
		else if (CurrentState == States.common2_1)  			{CommonRoom2_1 ();}
		else if (CurrentState == States.common2_2)  			{CommonRoom2_2 ();}
		else if (CurrentState == States.common3)  				{CommonRoom3 ();}
		else if (CurrentState == States.common4)  				{CommonRoom4 ();}
		else if (CurrentState == States.common5)  				{CommonRoom5 ();}
		else if (CurrentState == States.common6)  				{CommonRoom6 ();}
		// Bartender and Chris scene
		else if (CurrentState == States.bartenderStart)  		{BartenderStart ();}
		else if (CurrentState == States.bartenderUnsure)  		{BartenderUnsure ();}
		else if (CurrentState == States.drinkPurchase)  		{DrinkPurchase ();}
		else if (CurrentState == States.bartenderSighting)  	{BartenderSighting ();}
		else if (CurrentState == States.bartenderRecommend)  	{BartenderRecommend ();}
		else if (CurrentState == States.bartenderAdvice1)  		{BartenderAdvice1 ();}
		else if (CurrentState == States.bartenderAdvice2)  		{BartenderAdvice2 ();}
		else if (CurrentState == States.chrisStart)  			{ChrisStart ();}
		else if (CurrentState == States.chrisStory)  			{ChrisStory ();}
		else if (CurrentState == States.chrisBrambler)  		{ChrisBrambler ();}
		else if (CurrentState == States.chrisBefriend)  		{ChrisBefriend ();}
		else if (CurrentState == States.chrisMuscle)  			{ChrisMuscle ();}
		else if (CurrentState == States.chrisEnd)  				{ChrisEnd ();}
		// Gant Sequence
		else if (CurrentState == States.gantStart)  			{GantStart ();}
		else if (CurrentState == States.gantWhoIs1)  			{GantWhoIs1 ();}
		else if (CurrentState == States.gantWhoIs2)  			{GantWhoIs2 ();}
		else if (CurrentState == States.gantNotDrunk)  			{GantNotDrunk ();}
		else if (CurrentState == States.gantTooDrunk)  			{GantTooDrunk ();}
		else if (CurrentState == States.gantEnd)  				{GantEnd ();}
		//Daniela Sequence
		else if (CurrentState == States.danielaStart)  			{DanielaStart ();}
		else if (CurrentState == States.danielaWhere) 			{DanielaWhere ();}
		else if (CurrentState == States.danielaHelpful)			{DanielaHelpful ();}
		else if (CurrentState == States.danielaEnd)  			{DanielaEnd ();}
		//Drake Sequence
		else if (CurrentState == States.drakeStart)  			{DrakeStart ();}
		else if (CurrentState == States.drakeDeal)  			{DrakeDeal ();}
		else if (CurrentState == States.drakeCoerce)  			{DrakeCoerce ();}
		else if (CurrentState == States.theoDistract1)  		{TheoDistract1 ();}
		else if (CurrentState == States.theoDistract2)  		{TheoDistract2 ();}
		else if (CurrentState == States.drakeEating)  			{DrakeEating ();}
		else if (CurrentState == States.drakeEnd)  				{DrakeEnd ();}
		//Jess Sequence
		else if (CurrentState == States.jessStart)  			{JessStart ();}
		else if (CurrentState == States.jessYouOk)  			{JessYouOk ();}
		else if (CurrentState == States.jessMission)  			{JessMission ();}
		else if (CurrentState == States.bowRetrieve1)  			{BowRetrieve1 ();}
		else if (CurrentState == States.bowRetrieve2)  			{BowRetrieve2 ();}
		else if (CurrentState == States.bowDetails)  			{BowDetails ();}
		else if (CurrentState == States.bowEnd)  				{BowEnd ();}
		else if (CurrentState == States.jessCrazy)  			{JessCrazy ();}
		else if (CurrentState == States.jessEnd)  				{JessEnd ();}
		// brother sequence
		else if (CurrentState == States.brotherStart)  			{BrotherStart ();}
		else if (CurrentState == States.brotherWolfman)  		{BrotherWolfman ();}
		else if (CurrentState == States.brotherBartender)  		{BrotherBartender ();}
        else if (CurrentState == States.brotherInThere)         {BrotherInThere();}
        else if (CurrentState == States.brotherThreat)  		{BrotherThreat ();}
        else if (CurrentState == States.brotherSword)          {BrotherSword();}
        else if (CurrentState == States.brotherEnd)  			{BrotherEnd ();}
		else if (CurrentState == States.theoMutter)  			{TheoMutter ();}
		// End State
		else if (CurrentState == States.goToBed)				{GoToBed ();}
		#endregion
	}
	

	#region Intro State Methods
	void Intro_1 () {
		splash.text = "";
		title.text = "";
		horizontalline.text = "";
		verticalline.text = "";
		intro.text = "For months, you have been tracking a monster. It roams the countryside, slipping " + 
					"silently into into the houses of sleeping farmers and... feeding... with no sign of " + 
					"remorse. Many have underestimated this beast and paid for it with their lives. They " + 
					"don’t know the monster like you do, nor could they.\n\n" +
					"Because this bloodthirsty horror was once your brother. \n\nAnd you are going to kill it.\n\n" +
					"Press Space to continue";
		if (Input.GetKeyDown (KeyCode.Space)) 				{CurrentState = States.intro2;}
	}
	
	void Intro_2 () {
		intro.text = "You are less than a day behind your quarry. He was last spotted heading toward " + 
					"the town of Edgeton, and you are almost there. Your heart beats fast and you grip " + 
					"the hilt of your sword as you sprint the last 50 feet to the top of the hill. " + 
					"Finally, you crest the hill and behold your destination.\n\n" +
					"The sleepy town of Edgeton, which lies at the entrance to…\n\n" +
					"Press Space to continue";
		if (Input.GetKeyDown (KeyCode.Space)) 				{CurrentState = States.intro3;}
	}
	void Intro_3 () {
		intro.text = "";
		splash.text = "  /###           /  /                      /###           /                                                            ##            ##### ##                                                   \n" +
					  " /  ############/ #/                      /  ############/                      #                                       ##        ######  /### /                                                \n" +
					  "/     #########   ##                     /     #########                       ###                   #                  ##       /#   /  /  ##/                                           #     \n" +
					  "#     /  #        ##                     #     /  #     ##                      #                   ##                  ##      /    /  /    #                                           ##     \n" +
					  " ##  /  ##        ##                      ##  /  ##     ##                                          ##                  ##          /  /                                                 ##     \n" +
					  "    /  ###        ##  /##      /##           /  ###      ##    ###    ####    ###        /###     ######## /##      ### ##         ## ##       /###   ###  /###     /##       /###     ######## \n" +
					  "   ##   ##        ##/   ###  /   ###        ##   ##       ##     ###     ###/   ##     ##  ###/     ##   /   ###  ##   ####        ## ###### /   ###/   ##   ###/ /   ###   ##  ###/     ##     \n" +
					  "   ##   ##        ##     ## ##    ###       ##   ##       ##      ##      ##    ##    ####          ##  ##    ### ##    ##         ## ##### ##    ##    ##       ##    ### ####          ##     \n" +
					  "   ##   ##        ##     ## ########        ##   ##       ##      ##      ##    ##      ###         ##  ########  ##    ##         ## ##    ##    ##    ##       ########    ###         ##     \n" +
					  "    ##  ##        ##     ## #######          ##  ##       ##      ##      ##    ##        ###       ##  #######   ##    ##         #  ##    ##    ##    ##       #######       ###       ##     \n" +
					  "     ## #      /  ##     ## ##                ## #      / ##      ##      ##    ##          ###     ##  ##        ##    ##            #     ##    ##    ##       ##              ###     ##     \n" +
					  "      ###     /   ##     ## ####    /          ###     /  ##      /#      /     ##     /###  ##     ##  ####    / ##    /#        /####     ##    ##    ##       ####    /  /###  ##     ##     \n" +
					  "       ######/    ##     ##  ######/            ######/    ######/ ######/      ### / / #### /      ##   ######/   ####/         /  #####    ######     ###       ######/  / #### /      ##     \n" +
					  "         ###       ##    ##   #####               ###       #####   #####        ##/     ###/        ##   #####     ###         /    ###      ####       ###       #####      ###/        ##    \n" +
					  "                         /                                                                                                      #                                                               \n" +
					  "                        /                                                                                                        ##                                                             \n" +
					  "                       /                                                                                                                                                                        \n" +
					  "                      /                                                                                                                                                                         ";
		response.text = "Press Space to continue";
		
		if (Input.GetKeyDown (KeyCode.Space)) 				{CurrentState = States.edgeton;}
	}
	#endregion

	#region Into the Inn
	void Edgeton () {
		// Display UI for first time
		splash.text = "";
		title.text = @" ___                           ___                                                   _ ,                             " + "\n" +
					 @"-   ---___- ,,                -   ---___-                   ,         |\           ,- -                           ,  " + "\n" +
					 @"   (' ||    ||                   (' ||    ;       '        ||          \\         _||_                           ||  " + "\n" +
					 @"  ((  ||    ||/\\  _-_          ((  ||    \\/\/\ \\  _-_, =||=  _-_   / \\       ' ||    /'\\ ,._-_  _-_   _-_, =||= " + "\n" +
					 @" ((   ||    || || || \\        ((   ||    || | | || ||_.   ||  || \\ || ||         ||   || ||  ||   || \\ ||_.   ||  " + "\n" +
					 @"  (( //     || || ||/           (( //     || | | ||  ~ ||  ||  ||/   || ||         |,   || ||  ||   ||/    ~ ||  ||  " + "\n" +
					 @"    -____-  \\ |/ \\,/            -____-  \\/\\/ \\ ,-_-   \\, \\,/   \\/        _-/    \\,/   \\,  \\,/  ,-_-   \\, " + "\n" +
					 @"              _/                                                                                                     ";
		horizontalline.text = "------------------------------------------------------------------------------------------------------------------------------";
		verticalline.text = "|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n|";
		name.text = "Location:\nEdgeton Main Road";
		goldlabel.text = "GP:";
		gold = 650;
		
		text.text = "    You know your brother passed this way. He wouldn’t be in the town, of course, but someone may " + 
					"have seen him. With this in mind, you make your way into Edgeton to to ask around. \n\n    The sun is " + 
					"setting and many of the shops are closed up. The only sign of activity is coming from the Inn at the " + 
					"center of town. As you approach, the smell of roasted meat fills your nostrils. You can’t remember " +
					"the last time you ate.";
		response.text = "1. [Approach the inn]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1))		{CurrentState = States.innExamine;}
	}
	
	void InnExamine () {
		name.text = "Location:\nJolly Baler Inn";
		text.text = "    The inn lies at the center of town along the main road. The structure seems very old, with " +
					"cracks in the walls and a roof in clear need of repair. \n\n    Despite it’s questionable stability, the Inn’s wide " +
					"open doors are inviting. The sounds of laughter and singing can be heard inside, and a colorful " +
					"sign above the door reads, “Jolly Baler’s Inn.”";
		response.text = "1. [Enter the Inn]\n" + "2. [Back away from the Inn]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1))		{CurrentState = States.innkeepGreet;}
		else if (Input.GetKeyDown (KeyCode.Alpha2))		{CurrentState = States.edgeton;}
	}
	#endregion

	#region Talk to Jolly
	void InnKeepGreet () {
		name.text = "Talking to: \n" + innkeepName;
		text.text = "    Upon entering the inn, you see an incredibly muscular man polishing a giant axe. He sits in a chair " +
					"near the door and casts you a suspicious glance before turning back to his work. Meanwhile, a " +
					"portly man dressed in a dirty apron waves at you excitedly.\n\n" +
					"    “Greetings, traveller!” the man bellows, “what’s your pleasure?”";
		response.text = "1. “Who are you?”\n" + "2. “I need food and a room for the night.”\n" + "3. “I’m looking for someone.”";
		if 		(Input.GetKeyDown (KeyCode.Alpha1))		{CurrentState = States.innkeepWho;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			gold = gold - 12;
			CurrentState = States.roomRent;
		}else if (Input.GetKeyDown (KeyCode.Alpha3))		{CurrentState = States.innkeepRefuse;}
	}
	
	void InnKeepWho () {
		innkeepName = "Jolly Baler";
		name.text = "Talking to: \n" + innkeepName;
		text.text = "    “Me?” the portly man seems shocked that you need to ask, “Why I’m Jolly Baler, owner of " +
					"this fine establishment and your humble host. Er... provided you’re a paying customer, of course.”";
		response.text = "1. “I need food and a room for the night.”\n" + "2. “I’m looking for someone.”";
		if 	(Input.GetKeyDown (KeyCode.Alpha1)) {
			gold = gold - 12;
			CurrentState = States.roomRent;
		}else if (Input.GetKeyDown (KeyCode.Alpha2))		{CurrentState = States.innkeepRefuse;}
	}
	
	void RoomRent () {
		if (innkeepName == "?????????") {
			innkeepName = "Innkeeper";
			name.text = "Talking to: \n" + innkeepName;
		}
		text.text = "    “Excellent!” The innkeeper claps his hands together loudly, startling you slightly. “My beds are 10 " + 
					"gold pieces a night. If you need a hot meal, we can serve you in the common room for only 2 gold " +
					"more. First night’s payment is due up front.”\n\n" +
					"    You reach into your coinpurse and pay 12 gold for a bed and a hot meal. The innkeeper counts it " + 
					"twice before tucking it away behind the counter.\n\n    “Anything else?”";
		response.text = "1. “I'm looking for someone.”\n" + "2. “No that's it.” [Go to common room]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1))		{CurrentState = States.innkeepRefer;}
		else if (Input.GetKeyDown (KeyCode.Alpha2))		{CurrentState = States.common1;}
	}
	
	void InnKeepRefuse () {
		if (innkeepName == "?????????") {
			innkeepName = "Innkeeper";
			name.text = "Talking to: \n" + innkeepName;
		}
		text.text = "    “Well, I’d love to help you with that. Really, I would,” the innkeeper casts a glance toward the Inn’s " +
					"crowded common area as he continues, “But I’ve got my hands full with all the PAYING customers" +
					"I got.”\n\nYou wonder if maybe he is hinting at something...";
		response.text = "1. “I'd like to rent a room.”\n";
		if 	(Input.GetKeyDown (KeyCode.Alpha1)) {
			gold = gold - 12;
			CurrentState = States.roomRent;
		}
	}
	
	void InnKeepRefer () {
		chrisName = "Chris";
		text.text = "    You tell the innkeeper about the beast you seek and ask if there have been any sightings. \n\n" +
					"    “Oh, I don’t know about any of that,” he replies with a dismissive wave toward the man in the corner, " +
					"“but Chris here is always keeping tabs on the strange events around town. I’m sure if you-” The rattle of " +
					"falling pans in the kitchen interrupts the innkeeper and he rushes out of the room, " +
					"already yelling at his cook.\n\n    Chris hasn’t moved. There is no indication that he was even aware the innkeeper mentioned him.";
		response.text = "1. [Talk to Chris]\n" + "2. [Go to common room]\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1))		{CurrentState = States.chrisFirst;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.common1;}
	}
	#endregion

	#region Chris Intro
	void ChrisFirst () {
		name.text = "Talking to: \n" + chrisName;
		text.text = "    You turn to Chris. He makes no motion except the continued polishing of his axe. " +
					"You try clearing  your throat to get his attention. Finally, Chris looks up at you, " +
					"but with a glare that could curdle milk. You realize for the first time that there " +
					"is something strangely effeminate about him.\n\n" +
					"    “What?” Chris utters in a much higher voice than you would have expected.";
		response.text = "1. “Are you a woman?”\n" + "2. “I hear you are the one to talk to about monster sightings.”\n" + 
						"3. “Nevermind” [Go to common room]";
		if 	(Input.GetKeyDown (KeyCode.Alpha1)) {
			chrisGender = 1;
			chrisHeShe = "she";
			CurrentState = States.manOrWoman;
		}else if (Input.GetKeyDown (KeyCode.Alpha2))		{CurrentState = States.chrisMonsterSight;}
		else if (Input.GetKeyDown (KeyCode.Alpha3))		{CurrentState = States.common1;}
	}
	
	void ManOrWoman () {
		text.text = "    The moment the words escape your lips, you know they were a mistake. Chris stands up from his " +
					"(her?) chair, towering over you. Despite the manly physique, Chris moves with a grace you would " +
					"have expected of an aristocratic lady. In fact, you feel certain she may once have been.\n\n" +
					"    “Does it matter?” Chris practically barks the words. You sense that your response should be " +
					"chosen carefully.";
		response.text = "1. “Clearly not!”\n" + "2. “I was just wondering. No offense meant.”\n" + 
						"3. “I can think of a few reasons it might...”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)){
			chrisAttitude = 1;
			CurrentState = States.clearlyNot;
			}
		else if (Input.GetKeyDown (KeyCode.Alpha2))		{CurrentState = States.justWondering;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)){
			chrisAttitude = 2;
			CurrentState = States.chrisKnockout;
		}
	}
	
	void JustWondering () {
		text.text = "    “What do you want?” Chris seems only partially placated by your response.";
		response.text = "1. “I hear you are the one to talk to about monster sightings.”\n" + "2. “Nevermind” [Go to common room";
		if 		(Input.GetKeyDown (KeyCode.Alpha1))		{CurrentState = States.chrisMonsterSight;}
		else if (Input.GetKeyDown (KeyCode.Alpha2))		{CurrentState = States.common1;}
	}
	
	void ClearlyNot () {
		text.text = "    You say the words as genially as you can while gesturing to Chris’ towering physique. " +
					"The tiniest crack of a smile appears on Chris’ lips, followed by a burst of raucous laughter. " +
					"After a moment of shock, you find yourself laughing too.\n\n" +
					"    “I think I like you,” Chris claps you on the shoulder as she coninues, “what was it you wanted to ask me about?”";
		response.text = "1. “I hear you are the one to talk to about monster sightings.”\n" + "2. “Nothing.”";
		if 		(Input.GetKeyDown (KeyCode.Alpha1))		{CurrentState = States.chrisMonsterSight;}
		else if (Input.GetKeyDown (KeyCode.Alpha2))		{CurrentState = States.chrisNothing;}
	}
	
	void ChrisKnockout () {
		text.text = "    Chris interrupts you with a vicious left hook to the face that you probably should have seen coming. " +
					"The blow knocks you to the floor and the pain in your jaw is intense.\n\n" +
					"    “How about now?” Surprisingly, you know better than to answer the clearly rhetorical question. " +
					"Chris spits on you before picking up her axe and walking toward the common room.\n\n" +
					"You check your jaw. Nothing broken, but there will be a colossal bruise. Looks like you are gonna have " +
					"to find someone else to ask about your brother.";
		response.text = "1. [Go to common room]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.common1;}
	}
	
	void ChrisMonsterSight () {
			text.text = "    Chris listens intently as you describe the beast you are chasing.\n\n" +
						"    “I haven’t seen it myself,” Chris declares, “but I know someone who may have.” With that, " +
						chrisHeShe + " heads toward the common room and gestures for you to follow.";
		response.text = "1. [Go to common room]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) {
			chrisLead = 1;
			CurrentState = States.common1;
		}
	}
	
	void ChrisNothing () {
		text.text = "    Chris clearly doesn’t believe you, but she lets you off the hook anyways. “Come talk to me " +
					"again if you change your mind. I’ll be getting a drink.”\n\n" +
					"Chris heads toward the common room. Perhaps you should follow her.";
		response.text = "1. [Go to common room]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.common1;}
	}
	#endregion

	#region Common Room
	void CommonRoom1 (){
		// Advance round counter for passage of time
		commonRoomRounds = 1;
		
		string enterText;
		string chrisBrush = "";
		string chrisFemale = "";
		string barDesc;
		
		if (chrisGender == 0) {
			chrisFemale = "Despite the manly physique, " + chrisName + " moves with all the grace of an aristocratic lady. You wonder" +
				" not for the first time whether Chris is a man or a woman. ";
		}
		if (chrisLead == 1) {
			enterText = "    You follow Chris past the stairs and to the inn's modestly-sized common room. " + chrisFemale;
			barDesc = "\n\n    Chris is already at the bar and gesturing for you to come talk to the bartender. " +
						"You probably better go see what he has to say.";
		} else {
			enterText = "    You follow the smell of food past the stairs and to the inn's modestly-sized common room. ";
			chrisBrush = "\n\n    As you are taking in the scene, " + chrisName + " brushes past you and walks toward the bar. " + chrisFemale;
			barDesc = "\n\n    You also notice the bartender. He seems like the type to know what's going on.\n\n" +
					"    Maybe you should talk to him.";
		}
		
		
		string commonRoomDesc = enterText + "The room is filled with the smells and sounds of merriment and " +
								"conversation. Locals and travellers alike mingle at the room's many tables. " + 
								chrisBrush + barDesc;
		
		name.text = "Location:\nInn common area";
		text.text = commonRoomDesc;
		response.text = "1. [Approach the bar]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bartenderStart;}
	}
	
	void CommonRoom2_1 (){
		commonRoomRounds = 2;
		string charList;
		if (adviceTaken == 1){
			charList = "    Looking around the room, you quickly pick out the potential allies the bartender told you about. \n\n";
		} else {
			charList = "    In addition to Jess, there are a number of characters who catch your eye. " +
						"A knight, a quiet man, a young woman, and a reclusive man.\n\n";
		}
		string jessDesc = "    Jess is seated not far away. She seems a bit antsy and her lips are moving, " +
						"but there is no one with her to talk to.\n\n";
		string gantDesc = "    Leaning on the bar, you see " + gantName + " down half a mug's worth of ale in a single draught. " +
						"His weathered armor suggests considerable fighting experience... or perhaps poverty.\n\n";
		name.text = "Location:\nInn common area";
		text.text = charList + jessDesc + gantDesc;
		response.text = "1. [More]\n";
		if (Input.GetKeyDown (KeyCode.Alpha1)) 			{CurrentState = States.common2_2;}
	}
	
	void CommonRoom2_2 (){
		string danielaDesc = "    In the middle of the room, " + danielaName  + " has just finished talking to a gruff " +
							"looking man, and grimaces as he walks away. Almost as soon as he is gone, she glances furtively around " +
							"the room, clearly desperate for something. \n\n";
		string theoDesc = "    Finally, " + theoName  + " is noticeable for how everyone seems to give him a wide berth. His robes, " + 
							"once fine, are tattered and seem too big for him. There is something off about him that you can't seem to place. \n\n";
		string drakeDesc = "    Meanwhile, " + drakeName + " sits in an alcove with his back against the wall. His eyes " +
							"seem to see everything in the room despite not really looking at anything in particular.\n\n";
		text.text =  drakeDesc + danielaDesc + theoDesc + "    A motley line-up, for sure, but you will need help if you are to succeed.";
		response.text = "1. [Talk to " + jessName + "]\n" + 
						"2. [Talk to " + gantName + "]\n" + 
						"3. [Talk to " + drakeName + "]\n" + 
						"4. [Talk to " + danielaName + "]\n" + 
						"5. [Talk to " + theoName + "]\n";
		if (Input.GetKeyDown (KeyCode.Alpha1)) 			{CurrentState = States.jessStart;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.gantNotDrunk;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.drakeStart;}
		else if (Input.GetKeyDown (KeyCode.Alpha4)) 		{CurrentState = States.danielaStart;}
		else if (Input.GetKeyDown (KeyCode.Alpha5)) 		{CurrentState = States.theoMutter;}
	}
	
	void CommonRoom3 (){
		commonRoomRounds = 3;
		name.text = "Location:\nInn common area";
		if (jessRecruit >= 0) {
			text.text = "    You glance around the room again.\n\n    You notice " + gantName + " is still downing absurd amounts " +
						"of alcohol, but is somehow still cogent.\n\n    In the corner, " + drakeName + " is still watching " +
						"the room, but is also hastily scarfing down a plate of food he just recieved.\n\n    It takes you a moment to " +
						"spot " + danielaName + " again, but she seems just as lost as before, and " + theoName + " doesn't seem to " +
						"have moved at all.";
			response.text = "1. [Talk to " + gantName + "]\n" + 
							"2. [Talk to " + drakeName + "]\n" + 
							"3. [Talk to " + danielaName + "]\n" + 
							"4. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.gantStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.drakeEating;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.danielaStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha4)) 		{CurrentState = States.theoMutter;}
		} else if (drakeRecruit >= 0) {
			text.text = "    You glance around the room again.\n\n    You notice " + gantName + " is still downing absurd amounts " +
						"of alcohol, but is somehow still cogent.\n\n    It takes you a moment to " +
						"spot " + danielaName + " again, but she seems just as lost as before, and " + theoName + " doesn't seem to " +
						"have moved at all.\n\n    Also, you can see that " + jessName + " seems antsy and keeps looking toward the door.";
			response.text = "1. [Talk to " + gantName + "]\n" + 
							"2. [Talk to " + jessName + "]\n" + 
							"3. [Talk to " + danielaName + "]\n" + 
							"4. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.gantStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.jessStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.danielaStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha4)) 		{CurrentState = States.theoMutter;}
		}else if (danielaRecruit >= 0) {
			text.text = "    You glance around the room again.\n\n    You notice " + gantName + " is still downing absurd amounts " +
						"of alcohol, but is somehow still cogent.\n\n    In the corner, " + drakeName + " is still watching " +
						"the room, but is also hastily scarfing down a plate of food he just recieved.\n\n    Oddly, " + theoName + " doesn't seem to " +
						"have moved at all.\n\n    Also, you can see that " + jessName + " seems antsy and keeps looking toward the door.";
			response.text = "1. [Talk to " + gantName + "]\n" + 
							"2. [Talk to " + jessName + "]\n" + 
							"3. [Talk to " + drakeName + "]\n" + 
							"4. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.gantStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.jessStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.drakeEating;}
			else if (Input.GetKeyDown (KeyCode.Alpha4)) 		{CurrentState = States.theoMutter;}
		} else {response.text = "error";}
	}
	
	void CommonRoom4 (){
		commonRoomRounds = 4;
		name.text = "Location:\nInn common area";
		
		string drakeDanTheo = "    The common room has begun to thin out as it gets late and people begin going " +
							"home or to their rooms for the night.\n\n    Among those left, " + drakeName + 
							" still lurks, and " + danielaName + " is clearly losing hope of finding whatever it is she wants.\n\n" +
							"    You are unsurprised " + "to see that " + theoName + " is still here as well.";
		string gantDrakeTheo = "    The common room has begun to thin out as it gets late and people begin going " +
							"home or to their rooms for the night.\n\n    Teetering on unsteady legs, " + gantName + 
							" bravely soldiers on in his relentless pursuit of more alcohol.\n\n    Meanwhile, " + 
							drakeName + "still lurks.\n\n  You are unsurprised " + "to see that " + theoName + " is still here as well.";
		string gantDanTheo = "    The common room has begun to thin out as it gets late and people begin going " +
							"home or to their rooms for the night.\n\n    Teetering on unsteady legs, " + gantName + 
							" bravely soldiers on in his relentless pursuit of more alcohol.\n\n    Meanwhile, " + danielaName + " is clearly " + 
							"losing hope of finding whatever it is she wants.\n\n" +
							"    You are unsurprised " + "to see that " + theoName + " is still here as well.";
		string jessDanTheo = "    The common room has begun to thin out as it gets late and people begin going " +
							"home or to their rooms for the night.\n\n    You can see that " + jessName + 
							" is beginning to head home as well. This might be your last chance to talk to her.\n\n    Meanwhile, " + 
							danielaName + " is clearly losing hope of finding whatever it is she wants.\n\n" +
							"    You are unsurprised " + "to see that " + theoName + " is still here as well.";
		string jessGantTheo = "    The common room has begun to thin out as it gets late and people begin going " +
							"home or to their rooms for the night.\n\n    Teetering on unsteady legs, " + gantName + 
							" bravely soldiers on in his relentless pursuit of more alcohol.\n\n    You can see that " + 
							jessName + " is beginning to head home. This might be your last chance to talk to her.\n\n" +
							"    You are unsurprised " + "to see that " + theoName + " is still here as well."; 
		string jessDrakeTheo = "    The common room has begun to thin out as it gets late and people begin going " +
							"home or to their rooms for the night.\n\n    You can see that " + jessName + 
							" is beginning to head home as well. This might be your last chance to talk to her.\n\n    Among those left, " + 
							drakeName + "still lurks, and you are unsurprised " + "to see that " + theoName + " is still here as well.";

		if ((jessRecruit >= 0) && (gantRecruit >= 0)) {
			
			text.text = drakeDanTheo;
			response.text = "1. [Talk to " + drakeName + "]\n" + 
							"2. [Talk to " + danielaName + "]\n" + 
							"3. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.drakeStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.danielaStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.theoMutter;}
		}else if ((jessRecruit >= 0) && (danielaRecruit >= 0)) {
			text.text = gantDrakeTheo;
			response.text = "1. [Talk to " + gantName + "]\n" + 
							"2. [Talk to " + drakeName + "]\n" + 
							"3. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.gantTooDrunk;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.drakeStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.theoMutter;}
		}else if ((drakeRecruit >= 0) && (jessRecruit >= 0)) {
			text.text = gantDanTheo;
			response.text = "1. [Talk to " + gantName + "]\n" + 
							"2. [Talk to " + danielaName + "]\n" + 
							"3. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.gantTooDrunk;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.danielaStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.theoMutter;}
		}else if ((drakeRecruit >= 0) && (gantRecruit >= 0)) {
			text.text = jessDanTheo;
			response.text = "1. [Talk to " + jessName + "]\n" + 
							"2. [Talk to " + danielaName + "]\n" + 
							"3. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.jessStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.danielaStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.theoMutter;}
		}else if ((drakeRecruit >= 0) && (danielaRecruit >= 0)) {
			text.text = jessGantTheo;
			response.text = "1. [Talk to " + jessName + "]\n" + 
							"2. [Talk to " + gantName + "]\n" + 
							"3. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.jessStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.gantTooDrunk;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.theoMutter;}
		}else if ((danielaRecruit >= 0) && (gantRecruit >= 0)) {
			text.text = jessDrakeTheo;
			response.text = "1. [Talk to " + jessName + "]\n" + 
							"2. [Talk to " + drakeName + "]\n" + 
							"3. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.jessStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.drakeStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.theoMutter;}
		}
	}
	void CommonRoom5 (){
		commonRoomRounds = 5;
		name.text = "Location:\nInn common area";
		if ((drakeRecruit >= 1) && (danielaRecruit >= 1)) {
            text.text = "    Only a few folks remain. " + gantName + " has finally passed out from drunkeness, and " +
                        "Chris is dragging his limp body out of the room with a clear look of disgust on her face.\n\n    " +
                        "Your grumbling stomach reminds you that you still haven't eaten. Curiously, the bartender is " +
                        "nowhere to be seen. You seek out the cook instead.";
            response.text = "1. [Eat]\n";
            if      (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.common6; }
        }
        else if ((drakeRecruit >= 1) && !(danielaRecruit >= 1)) {
			text.text = "    Only a few folks remain. " + gantName + " has finally passed out from drunkeness, and " +
						"Chris is dragging his limp body out of the room with a clear look of disgust on her face.\n\n    " +
                        "Curiously, the bartender is nowhere to be seen, but you don't have much time to think about it, because " +
						"you've caught a glimpse of " + danielaName + " walking straight toward you.\n\n    “Excuse me!” she " +
						"calls out, “may I have a moment of your time?”";
			response.text = "1. [Talk to " + danielaName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.danielaStart;}
		}else if (!(drakeRecruit >= 1) && (danielaRecruit >= 1)) {
			text.text = "    Only a few folks remain. " + gantName + " has finally passed out from drunkeness, and " +
						"Chris is dragging his limp body out of the room with a clear look of disgust on her face.\n\n    " +
                        "Curiously, the bartender is nowhere to be found.\n\n    Of your potential allies, only " + drakeName + " remains.";
			response.text = "1. [Talk to " + drakeName + "]\n" + 
							"2. [Talk to " + theoName + "]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.drakeStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.theoMutter;}
		}
	}
	void CommonRoom6 (){
		commonRoomRounds = 6;
		name.text = "Location:\nInn common area";
		text.text = "    After a hearty meal and a mug of ale, you realize the room has fallen strangely quiet. The bartender has finally returned, but he is the only one left in the room besides you, making the crackling of " +
                    "the fire and the soft swishing of the bartender's " +
	            	"broom the only sounds to be heard.\n\n    You are feeling sleepy as well, but the cushioned chairs near the fireplace seem so inviting...";
		response.text = "1. [Sit by the fire]\n" +
						"2. [Go to bed]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.brotherStart;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{
            attemptSleep = true;
            CurrentState = States.brotherStart;
        }
	}
	#endregion
	
	#region bartender and chris
	void BartenderStart (){
		name.text = "Talking to: \nGil";
		if (chrisLead == 1) {
			text.text = "    As you approach the bar, Chris gestures toward you while addressing " +
						"the bartender.\n\n    “Here they come,” Chris announces, “Seem's somebody's come looking for " + 
						"your monster, Gil.” The bartender finishes serving a drink before looking you over.\n\n    " +
						"“Finally,” the bartender utters, “Somebody who believes me!” The bartender quickly describes " +
						"the creature he saw, and after listening, you have no doubt that he saw your brother.";
			response.text = "1. “Tell me everything.”\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bartenderSighting;}
		} else {
			text.text = "    As you approach the bar, you overhear " + chrisName + " talking to the bartender.\n\n    " +
						"“Any new sightings, Gil?” " + chrisName + " asks.\n\n    “Don't make fun, Chris,” the bartender " +
						"replies, “I'm telling you, It walked like a man, but had a face like a wolf. You know me, I wouldn't make this up!”\n\n    " + 
						chrisName + " only laughs, but you feel a chill go down your spine. He's seen your brother.";
			response.text = "1. “Did you see where it went?”\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{
				chrisName = "Chris";
				CurrentState = States.bartenderUnsure;
			}
		}
	}
	
	void BartenderUnsure (){
		text.text = "    Gil is surprised by your interruption and looks you up and down, as though unsure of your " +
					"motives. You quickly assure him that you believe him, and that you have seen the monster too.\n\n    " +
					"“Finally!” the bartender exclaims, “Someone with some sense.”";
		response.text = "1. “Tell me everything.”";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bartenderSighting;}
	}
	
	void BartenderSighting (){
		text.text = "    Gil talks at length about what he was doing and where he was going before finally getting " +
					"around to describing his sighting.\n\n    “And there it was,” he says with a dramatic wave of his arms, " +
					"“the wolf-man looked straight into my soul - my SOUL I tell you! - before skulking away into the...” here " +
					"the bartender leans in and speaks with a lower voice, “...the Twisted Forest.”\n\n    You are only vaguely " +
					"familiar with the twisted forest, but you know enough to sense that things just got much more complicated.";
		response.text = "1. “Well, I guess I'm going into the forest, then...”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bartenderRecommend;}
	}
	
	void BartenderRecommend (){
		text.text = "    “Wait,” Gil seems aghast, “You're not planning on going after it? By yourself?” He doesn't wait " +
					"for your answer before plowing on. “Well, that won't do at all. No sane person just goes traipsing into the " +
					"Twisted Forest - leastways not ALONE. That's not to say sane people go chasing after wolf-men in the first place, but " +
					"you're still gonna need help.”";
		response.text = "1. “What do you recommend?”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bartenderAdvice1;}
	}
	
	void BartenderAdvice1 (){
		jessName = "Jess";
        chrisName = "Chris";
		text.text = "    “First thing you'll need is a guide. See that lady over there?” Gil " +
					"points toward a plainly dressed woman who is eating alone on the other side of the room, “That's Jess. She spends " +
					"more time in the forest than anybody. Knows it like the back of her hand.” He pauses momentarily. “Careful " +
					"though: she's a bit touched.” he taps his temple in a gesture clearly meant to imply insanity.\n\n    “Of course, if you " +
					"prefer the company of those in their right mind, I can always recommend my good friend, Chris!” he gestures toward " +
					"the hulking figure who has likely been listening in to the whole conversation, “She's not as familiar with the forest, " +
					"but she's guided a few folks for the right price!”\n\n    “Don't volunteer me, Gil!” Chris interjects.";
		response.text = "1. “I think I need a drink.”\n" +
						"2. “Any other advice?”\n" +
						"3. [Talk to Chris]\n" +
						"4. [Step away from the bar]\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{
			gold = gold - 1;
			CurrentState = States.drinkPurchase;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.bartenderAdvice2;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.chrisStart;}
		else if (Input.GetKeyDown (KeyCode.Alpha4)) 		{CurrentState = States.common2_1;}
	}
	
	void BartenderAdvice2 (){
		if ((drinkPurchase == 1) || (chrisAttitude == 1)){
			adviceTaken = 1;
			gantName = "Sir Gant";
			danielaName = "Daniela";
			drakeName = "Drake";
			theoName = "Theodore";
			text.text = "    “Well, there's a few other folks here who might be willing to travel with you.”\n\n    Gil points out " +
						"several people in the room who might be of help: Sir Gant the knight, Drake (who Gil suspects is a thief), " +
						"the Lady Daniela, and a mysterious recluse named Theodore.\n\n    You thank him for his help.";
			response.text = "1. [Talk to Chris]\n" +
							"2. [Step away from the bar]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.chrisStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.common2_1;}
		} else {
			text.text = "    Gil only shrugs, “Afraid that's all I got, right now. How about a drink?”\n\n    " +
						"You suspect he actually DOES have more to say...";
			response.text = "1. “Sure, I'll take a drink.”\n" +
							"1. [Talk to Chris]\n" +
							"2. [Step away from the bar]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{
				gold = gold - 1;
				CurrentState = States.drinkPurchase;
			}
			if 		(Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.chrisStart;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.common2_1;}
		}
	}
	
	void DrinkPurchase (){
		drinkPurchase = 1;
		text.text = "    Gil drops a mug of frothy ale in front of you. You give it a sip and are pleasantly surprised by its rich, " +
					"smooth flavor.\n\n    “Anything else?” he asks.";
		response.text = "1. “Any other advice?”\n" +
						"2. [Talk to Chris]\n" +
						"3. [Step away from the bar]\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bartenderAdvice2;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.chrisStart;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.common2_1;}
	}
	
	void ChrisStart (){
		name.text = "Talking to: \nChris";
		if (chrisAttitude == 2) {
			text.text = "    Chris shoots you an icy glare as you approach. “I've got nothing to say " +
						"to you.”\n\n    It's probably best to move on."; 
			response.text = "[Step away from the bar]";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.common2_1;}
		} else {
			text.text = "    Chris turns in her stool to face you as you approach. \n\n    “I can't say " +
						"I would've pegged you for a brambler, but you've got my attention.”"; 
			response.text = "1. “Brambler?”\n" +
							"2. “What's your story?”\n" +
							"3. “I'm looking for some muscle.”\n" +
							"4. “Nevermind”\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.chrisBrambler;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.chrisStory;}
			else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.chrisMuscle;}
			else if (Input.GetKeyDown (KeyCode.Alpha4)) 		{CurrentState = States.common2_1;}
		}
	}

	void ChrisBrambler (){
		name.text = "Talking to: \nChris";
		text.text = "    Chris laughs at your question, but you're not sure why it's funny.\n\n    “It's just " +
					"our nickname for the outsiders who come around looking to try their luck in the forest.”";
		response.text = "1. “What's your story?”\n" +
					"2. “I'm looking for some muscle.”\n" +
					"3. “Nevermind”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.chrisStory;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.chrisMuscle;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.common2_1;}
	}
	
	void ChrisStory (){
		text.text = "    Your question seems to set Chris on edge. Her face hardens into a scowl and her " +
					"eyebrows raise in suspicion.\n\n    “My story? What do you mean?”";
		response.text = "1. “What drives a woman to dress like a man and carry a giant axe around?”\n" +
						"2. “Everyone's got a story. I just want to know yours.”\n" +
						"3. “Nevermind”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) {
			chrisAttitude = 0;
			CurrentState = States.chrisBefriend;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			chrisAttitude = 1;
			CurrentState = States.chrisBefriend;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.common2_1;}
	}
	
	void ChrisMuscle (){
		if (chrisAttitude == 0) {
			text.text = "    “Well, Jolly's been paying me well to keep the door here at the inn. But " +
						"maybe I could be convinced to leave for, say, 500 gold?”\n\n    That price is a " +
						"bit steep, but Chris doesn't seem the type to haggle.";
			response.text = "1. “You're hired! [pay 500 gold]”\n" +
							"2. “Nevermind”\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) {
				chrisRecruit = 2;
				gold = gold - 500;
				CurrentState = States.chrisEnd;
			}else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.common2_1;}
		}else if (chrisAttitude == 1) {
			text.text = "    “I'll tell you what,” Chris allows herself a slight smile as she continues, " +
						"“I like you, so I'm gonna cut you a deal. I'll accompany you into the forest and help " +
						"you find your monster, no strings attached... only 350 gold.”\n\n    You aren't sure if " +
						"that's actually a deal, but Chris seems to think it is.";
			response.text = "1. “You're hired! [pay 350 gold]”\n" +
							"2. “Nevermind”\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) {
				chrisRecruit = 2;
				gold = gold - 350;
				CurrentState = States.chrisEnd;
			}else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.common2_1;}
		}
	}
	
	void ChrisBefriend (){
		if (chrisAttitude == 0) {
			text.text = "    “Revenge,” is her one word reply. It's probably the only one you will get.";
		}else if (chrisAttitude == 1) {
			text.text = "    Chris doesn't respond immediately. She seems far away... as though deep in some " +
						"near-forgotten memory.\n\n    “Let's just say I lost someone,” she replies finally, “and " +
						"if I ever find the bastards that took him from me, they won't have the luxury of asking " +
						"inane questions.”\n\n    You suppose that is as close to a friendly answer as you are going to get right now. ";
		}
		response.text = "1. “I'm looking for some muscle.”\n" +
						"2. “Nevermind”\n";
		if 		 (Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.chrisMuscle;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.common2_1;}
	}
	
	void ChrisEnd (){
		text.text = "    “Wonderful!” for a brief moment, Chris seems giddy like a litte girl, but she is back to business " +
					"just as quickly. “I will prepare myself tonight and meet you back here in the morning.”";
		response.text = "1. “See you tomorrow.”\n";
		if (Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.common2_1;}
	}
	#endregion
	
	#region Gant
	void GantStart () {
		name.text = "Talking to: \n" + gantName;
		text.text = "    You take a seat at the bar next to " + gantName + " before casually attempting to get his attention.\n\n" +
					"    “I might just be drunk enough to talk to you,” " + gantName + " says good naturedly. He doesn't seem very drunk at all, actually.";
		response.text = "1. “Who are you?”\n" +
						"2. “I am looking for travelling companions...”\n" +
						"3. “Nevermind”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.gantWhoIs1;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.gantEnd;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.common4;}
	}
	
	void GantWhoIs1 (){
		gantName = "Sir Gant";
		name.text = "Talking to: \n" + gantName;
		text.text = "    “Ha! You've not heard of me?” " + gantName + " seems surprised, but happy to educate you. “I, of " +
					"course, am Sir Gant of the Wilk.”\n\n    You've never heard of the Wilk before, much less Sir Gant. You " +
					"wonder whether it is wise to tell him so.";
		response.text = "1. “Of course! I am pleased to meet you, Sir Gant of the Wilk.”\n" +
						"2. “The Wilk?”\n" +
						"3. “Nevermind”\n";
		if (Input.GetKeyDown (KeyCode.Alpha1)) 			{
			gantKnown = 1;
			CurrentState = States.gantWhoIs2;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.gantWhoIs2;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) 		{CurrentState = States.common4;}
	}
	
	void GantWhoIs2 (){
		if (gantKnown == 1) {
			text.text = "    “And you!” Sir Gant seems genuinely happy with your response, “Tell me, you wouldn't be heading into the forest, would you?”";
		} else {
			text.text = "    “Well of course YOU wouldn't have heard of it.” Gant polishes of the last bit of ale in his mug " +
						"before continuing, “just another uneducated provincial here to get himself killed in the forest.”";
		}
		response.text = "1. “Well, I AM heading into the forest...”\n" +
						"2. “Nevermind”\n";
		if (Input.GetKeyDown (KeyCode.Alpha1)) 			{CurrentState = States.gantEnd;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.common4;}
	}
	
	void GantNotDrunk (){
		name.text = "Talking to: \n" + gantName;
		text.text = "    You approach " + gantName + " and attempt to strike up a conversation.\n\n    “Leave me alone,” he grumbles. " +
					"You notice he avoids all eye contact and seems generally uncomfortable with your presence.\n\n    “I'm not " +
					"drunk enough for this,” he mutters under his breath.\n\n    It's probably best to leave him to his drinking.";
		response.text = "1. [back away from " + gantName + " ]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.common2_1;}
	}
	
	void GantTooDrunk (){
		name.text = "Talking to: \n" + gantName;
		text.text = "    Even before you reach him, you can tell " + gantName + "is completely sloshed. The bartender is diplomatically " +
					"attempting to cut him off for the night.\n\n    “More!” " + gantName + " bellows as he clumsily draws his sword, nearly " +
					"lopping off a bystander's head in the process. “I'll decide when I've had enough.”\n\n    The bartender defuses the " +
					"situation with another drink. You decide to keep your distance.";
		response.text = "1. [back away from " + gantName + " ]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) {CurrentState = States.common4;}
	}
	
	void GantEnd (){
		gantName = "Sir Gant";
		gantRecruit = 2;
		text.text = "    “Excellent!” Sir Gant slams his mug down with gusto, startling the bartender, “You could be just " +
					"the companion I need. Gather the rest of your party and meet me by the stables at sunrise tomorrow.”\n\n    " +
					"Despite the fact you never actually asked him to come along, a knight and his horse could be useful. " +
					"At the rate he's drinking, though, there is a good chance he won't remember this conversation in the morning.";
		response.text = "1. “I hope I don't regret this...” [Back to room]\n";
		if (Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.common4;}
	}
	#endregion
	
	#region Daniela
	void DanielaStart () {
		danielaName = "Daniela";
		name.text = "Talking to: \n" + danielaName;
		text.text = "    Having introduced herself to you as Lady Daniela, the woman wastes no time in petitioning you.\n\n    “I am in " +
					"desperate need of assistance. I seek passage through the twisted forest, but no one here seems willing to assist " +
					"me.”\n\n    Now that she is close, you notice that " + danielaName + " wears a triangular charm around her neck, " +
					"marking her as a devotee of the White Priests. The priests are known for their healing and protection magic. If " +
					"she has any of their power, she may prove useful...";
		response.text = "1. “Where are you trying to go exactly?”\n" +
						"2. “Why should I help you?”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) {CurrentState = States.danielaWhere;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {CurrentState = States.danielaHelpful;}

	}
	
	void DanielaHelpful () {
		text.text = "    Daniela seems tired for just a moment but regains her composure quickly.\n\n    “I'm afraid it's true " +
					"that I have no money to offer,” she says sadly, “But I am skilled as a healer and quite capable of taking care of myself!” " +
					"She pats the dagger at her side as she says this.";
        if (danielaSecondQuestion)
        {
            response.text = "1. “Ok, but you better not slow me down.”\n" +
                    "2. “Nevermind”\n";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.danielaEnd; }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (commonRoomRounds == 2)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common3;
                }
                else if (commonRoomRounds == 3)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common4;
                }
                else if (commonRoomRounds == 4)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common5;
                }
                else if (commonRoomRounds == 5)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common6;
                }
            }
        } else
        {
            response.text = "1. “Ok, but you better not slow me down.”\n" +
                    "2. “Where are you trying to go, exactly?”\n" +
                    "3. “Nevermind”\n";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.danielaEnd; }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                danielaSecondQuestion = true;
                CurrentState = States.danielaWhere;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (commonRoomRounds == 2)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common3;
                }
                else if (commonRoomRounds == 3)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common4;
                }
                else if (commonRoomRounds == 4)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common5;
                }
                else if (commonRoomRounds == 5)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common6;
                }
            }
        }  
	}
	
	void DanielaWhere () {
		text.text = "    “I'm afraid I can't discuss that,” she says cautiously. “But I assure you my errand is of the utmost importance.”";
        if (danielaSecondQuestion)
        {
            response.text = "1. “Ok, but you better not slow me down.”\n" +
                    "2. “Nevermind”\n";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.danielaEnd; }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (commonRoomRounds == 2)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common3;
                }
                else if (commonRoomRounds == 3)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common4;
                }
                else if (commonRoomRounds == 4)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common5;
                }
                else if (commonRoomRounds == 5)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common6;
                }
            }
        } else
        {
            response.text = "1. “Ok, but you better not slow me down.”\n" +
                    "2. “Why should I help you?”\n" +
                    "3. “Nevermind”\n";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.danielaEnd; }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                danielaSecondQuestion = true;
                CurrentState = States.danielaHelpful;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (commonRoomRounds == 2)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common3;
                }
                else if (commonRoomRounds == 3)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common4;
                }
                else if (commonRoomRounds == 4)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common5;
                }
                else if (commonRoomRounds == 5)
                {
                    danielaRecruit = 1;
                    CurrentState = States.common6;
                }
            }
        }
	}
	
	void DanielaEnd () {
		danielaRecruit = 2;
		text.text = "    “Oh, thank you! Thank you!” Daniela appears as though she could hug you, but you know she is too " +
					"proper for that.\n\n    “I will retire now and meet you here in the morning,” she adds enthusiastically.";
		response.text = "1. “Good night, milady”\n";
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if 		(commonRoomRounds == 2)			{CurrentState = States.common3;}
			else if (commonRoomRounds == 3)			{CurrentState = States.common4;}
			else if (commonRoomRounds == 4)			{CurrentState = States.common5;}
			else if (commonRoomRounds == 5)			{CurrentState = States.common6;}
		}
	}
	#endregion
	
	#region Drake
	void DrakeStart () {
		name.text = "Talking to: \n" + drakeName;
		text.text = "    “Take a seat,” " + drakeName + " seems as though he was expecting you.";
		response.text = "1. [Take a seat]\n" +
						"2. “Nevermind”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.drakeDeal;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if 		(commonRoomRounds == 2)			{CurrentState = States.common2_1;}
			else if (commonRoomRounds == 3)			{CurrentState = States.common3;}
			else if (commonRoomRounds == 4)			{CurrentState = States.common4;}
			else if (commonRoomRounds == 5)			{CurrentState = States.common5;}
		}
	}
	
	void DrakeEating () {
		name.text = "Talking to: \n" + drakeName;
		text.text = "    You are a few steps away when " + drakeName + " freezes in the middle of " +
					"taking a bite from a hunk of bread. He glares at you intensely.\n\n    “Can't you " +
					"see I'm eating?” he sounds exasperated, “come back later.”\n\n    Far be it from you to come between a man and his meal...";
		response.text = "1. [Go back to room]\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.common3;}
	}
	
	void DrakeDeal () {
		drakeName = "Drake";
		theoName = "Theodore";
		text.text = "    “I'm glad you finally came my way” " + drakeName + " doesn't seem to look at you as " +
					"he talks. Instead, his eyes seem to be constantly scanning the room behind you. “The name's " +
					"Drake, and I think we can help eachother. You see Theodore over there?”, Drake gestures to the reclusive man, “He's got something " +
					"that belongs to me. You help me get it back, and I'll help you find your wolf-man.”\n\n    " +
					"You don't know how he knows about your quest, but Drake is clearly a man of considerable skill.";
		response.text = "1. “I'm in.”\n" +
						"2. “You want me to help you steal?”\n" +
						"2. “Nevermind”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.theoDistract1;}
		if 		(Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.drakeCoerce;}
		else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			if 		(commonRoomRounds == 2)			{CurrentState = States.common2_1;}
			else if (commonRoomRounds == 3)			{CurrentState = States.common3;}
			else if (commonRoomRounds == 4)			{CurrentState = States.common4;}
			else if (commonRoomRounds == 5)			{CurrentState = States.common5;}
		}
	}
	
	void DrakeCoerce () {
		text.text = "    “Liberate,” Drake seems annoyed at your use of the word 'steal', “And all I need is a distraction. " +
					"Just keep him talking and I do all the dirty work.”";
		response.text = "1. “Ok, I'll help you.”\n" +
						"2. “I'm not getting involved in this.” [Back to common room]\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.theoDistract1;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if 		(commonRoomRounds == 2)			{CurrentState = States.common2_1;}
			else if (commonRoomRounds == 3)			{CurrentState = States.common3;}
			else if (commonRoomRounds == 4)			{CurrentState = States.common4;}
			else if (commonRoomRounds == 5)			{CurrentState = States.common5;}
		}
	}
	
	void TheoDistract1 () {
		text.text = "    After some quick instructions from Drake about where to stand, you make your way over to Theodore. " +
					"As you approach, you notice that the recluse seems to be fiddling with something hidden in his sleeve, " +
					"and a faint, bluish light emanates from it. At his feet, you see a small bag - undoubtedly Drake's target.\n\n    Theodore " +
					"notices your presence and casts you a suspicious glance. The bluish light has disappeared.";
		response.text = "1. “Oooh! what was that thing?”\n" +
						"2. “I came to warn you...”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) {
			drakeRecruit = 2;
			CurrentState = States.theoDistract2;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			drakeRecruit = 1;
			CurrentState = States.drakeEnd;
		}
	}
	
	void TheoDistract2 () {
		
		if (drakeRecruit == 1) {
			text.text = "    You quickly warn Theodore that someone here is planning on stealing from him. He picks up his " +
						"bag from the floor and holds it tightly on his lap, but he seems just as suspicious of you as before.\n\n    " +
						"“Yes, yes,” He mumbles, “Now leave me be.”\n\n    Not even a thank you...";
			response.text = "1. [Walk away]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) {CurrentState = States.drakeEnd;}
		} else {
			text.text = "    You attempt to make small talk with Theo, but he is surprisingly unresponsive. He glares at you with " +
						"suspicion, but answers none of your questions.\n\n    After a few more awkward attempts, you notice that " +
						"the bag at Theodore's feet is gone. You never even saw Drake take it.";
			response.text = "1. [Walk away]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) {CurrentState = States.drakeEnd;}
		}
	}
	
	void DrakeEnd () {
		if (drakeRecruit == 2) {
			text.text = "    “Well done,” Drake " +
						"whispers as you return, “As promised, I am at your service for the journey ahead. I will " +
						"find you in the morning.”\n\n    Without another word, Drake slips away, presumably to sleep.";
			response.text = "1. [Back to room]\n";
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if 		(commonRoomRounds == 2)			{CurrentState = States.common3;}
				else if (commonRoomRounds == 3)			{CurrentState = States.common4;}
				else if (commonRoomRounds == 4)			{CurrentState = States.common5;}
				else if (commonRoomRounds == 5)			{CurrentState = States.common6;}
			}
		}else if (drakeRecruit == 1) {
			text.text = "    As you walk away from Theodore, you catch a glimpse of Drake moving toward the door. He glances back " +
						"at you and makes a rude gesture before departing.\n\n    Looks like you won't be getting any help from him.";
			response.text = "1. [Back to room]\n";
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if 		(commonRoomRounds == 2)			{CurrentState = States.common3;}
				else if (commonRoomRounds == 3)			{CurrentState = States.common4;}
				else if (commonRoomRounds == 4)			{CurrentState = States.common5;}
				else if (commonRoomRounds == 5)			{CurrentState = States.common6;}
			}
		}
	}
	#endregion
	
	#region Jess
	void JessStart () {
		name.text = "Talking to: \n" + jessName;
		text.text = "    As soon as Jess notices you, her lips stop moving and she makes " +
					"uncomfortably long eye contact as you close the distance.\n\n    “Good Evening,” she says pleasantly, “Jess " +
					"Woodrun, at your service.”\n\n    You are about to reply in kind when the woman turns to the " +
					"empty chair next to her and puts a finger to her lips.\n\n    “Shhh!” she hisses, “I'm trying to have a conversation.”";
		response.text = "1. “Are you ok?”\n" +
						"2. “Umm... I'm just gonna go now.” [Return to room]\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.jessYouOk;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if 		(commonRoomRounds == 2)			{CurrentState = States.common3;}
			else if (commonRoomRounds == 3)			{CurrentState = States.common4;}
		}
	}
	
	void JessYouOk () {
		text.text = "    “I'm fine, thank you,” Jess leans back in her chair and tilts her head back in a gesture that seems " +
					"simultaneously haughty and uncertain, “What was it you wanted to ask me?”";
		response.text = "1. “I'm looking for a forest guide.”\n" +
						"2. “Nevermind”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.jessMission;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			if 		(commonRoomRounds == 2)			{CurrentState = States.common3;}
			else if (commonRoomRounds == 3)			{CurrentState = States.common4;}

		}
	}
	
	void JessMission () {
		text.text = "    “Oh, I see,” Jess looks genuinely dissapointed, “It would be my pleasure to help you - really it would - " +
					"but I'm short a bow. I ordered a new one recently, but now, the local weaponmaster " +
					"won't speak to me for some reason.”\n\n    You begin to reply, but Jess again interrupts you to yell at the chair.\n\n" +
					"    “WILL YOU SHUT UP! I can't hear what they're saying!” She clears her throat before turning back to you. “Anyway, I " +
					"was wondering if maybe you might pick up the bow for me? The shop is open late, so if you get it for me tonight, I could " +
					"be ready to guide you tomorrow.”";
		response.text = "1. “I'll go right now.”\n" +
						"2. “I'm not an errand boy”\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bowRetrieve1;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.jessEnd;}
	}
	
	void BowRetrieve1 () {
		name.text = "Location:\nEdgeton Weapon Shop.";
		text.text = "    It takes you some time to locate the weaponshop. When you finally do, you are dissapointed to see the large wooden " +
					"doors closed. There is still a light on inside, however, and the door doesn't appear to be locked.";
		response.text = "1. [Try the door]\n" +
						"2. [Give up]\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bowRetrieve2;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			jessRecruit = 1;
			CurrentState = States.jessEnd;
		}
	}
	
	void BowRetrieve2 () {
		name.text = "Talking to:\nWeapon's merchant.";
		text.text = "    You push the door open, startling the elderly weapons master inside.\n\n    “We're closed!” he barks, “What do you mean " +
					"by barging in here so late?” You quickly explain your errand, but the old man rolls his eyes.\n\n    “THIS again,” he " +
					"grumbles, “350 gold. That was the price we agreed upon, and I'll not give her that bow till it's paid. I have costs, you " +
					"know. it's not like bows just grow on trees.”\n\n    If the old man is aware of the slight irony in that statement, he doesn't let on.";
		if (gold >= 250) {
			response.text = "1. [Haggle]\n" +
							"2. [Pay 250 Gp]\n" +
							"3. [Leave]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bowDetails;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				jessRecruit = 2;
				gold = gold - 250;
				CurrentState = States.bowEnd;
			}else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				jessRecruit = 1;
				CurrentState = States.jessEnd;
			}
		} else {
			response.text = "1. [Haggle]\n" +
							"2. [Leave]\n";
			if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.bowDetails;}
			else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				jessRecruit = 1;
				CurrentState = States.jessEnd;
			}
		}
	}
	
	void BowDetails () {
		text.text = "    You haggle with the old weapons master for several minutes, but you only manage to talk him down a small amount.\n\n    " +
					"“200 gold,” he says with finality, “that's the best you'll get from me.”";
		if (gold >= 200) {
			response.text = "1. [Pay 200 Gp]\n" +
							"2. [Leave]\n";
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				jessRecruit = 2;
				gold = gold - 200;
				CurrentState = States.bowEnd;
			}else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				jessRecruit = 1;
				CurrentState = States.jessEnd;
			}
		} else {
			response.text = "1. [Leave]\n";
			 if (Input.GetKeyDown (KeyCode.Alpha1)) {
				jessRecruit = 1;
				CurrentState = States.jessEnd;
			}
		}
	}
	
	void BowEnd () {
		text.text = "    With a good deal more grumbling, the old man digs around in the back of the store for several minutes. " +
					"He finally emerges carrying an ornately decorated bow and sets it unceremoniously on the counter.\n\n    “One " +
					"rosewood bow, blessed by the white priests themselves,” he announces dryly, “Now get out.”";
		response.text = "1. [Return to Jess]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) {CurrentState = States.jessCrazy;}
	}
	
	void JessCrazy () {
		text.text = "    You return to Jess and give her the bow. There is a fire in her eyes as she runs her finger over the decorative " +
					"markings.\n\n    “Ha!” quick as a flash of lightning, Jess nocks an arrow from the quiver on her hip and fires in " +
					"the direction of the empty chair. The room falls momentarily silent as most of the Inn's patrons crane their necks " +
					"to see what's going on.\n\n    “Did'ya hit it this time, lass?” a voice calls out mockingly. The room erupts in a round " +
					"of laughter. Jess' shoulders slump, and for a moment she seems utterly defeated.\n\n    “Blessed by the white priests, " +
					"my ass,” Jess mutters to herself before turning to you. “Now, about my fee...”";
		response.text = "1. “Your fee? I just paid for your bow!”";
		if (Input.GetKeyDown (KeyCode.Alpha1)) {CurrentState = States.jessEnd;}
	}
	
	void JessEnd () {
		name.text = "Talking to: \n" + jessName;
		if (jessRecruit == 1) {
			text.text = "    You return to Jess empty-handed. She dismisses you with a wave of her hand.\n\n    “You're just like the rest of " +
						"them,” she says with a snarl.";
			response.text = "1. [Back to room]\n";
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if 		(commonRoomRounds == 2)			{CurrentState = States.common3;}
				else if (commonRoomRounds == 3)			{CurrentState = States.common4;}
			}
		} else if (jessRecruit == 0) {
			text.text = "    “Suit yourself, then.”";
			response.text = "1. [Back to room]\n";
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if 		(commonRoomRounds == 2)			{CurrentState = States.common3;}
				else if (commonRoomRounds == 3)			{CurrentState = States.common4;}
			}
		}
		else {
			text.text = "    “Really?” Jess feigns surprise, “You let that wiley old man talk you out of your money?” She pauses a moment before " +
						"continuing. “I suppose I could wave my fee for your trouble... provided you provide us with adequate provisions.”";
			response.text = "1. “Fine.”\n";
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if 		(commonRoomRounds == 2)			{CurrentState = States.common3;}
				else if (commonRoomRounds == 3)			{CurrentState = States.common4;}
			}
		}
	}
    #endregion

    #region Brother
    void BrotherStart () {
        if (attemptSleep == true) {
            name.text = "Location: \nCommon Room";
            text.text = "    As you are getting up to leave, Gil, the bartender, moves to intercept you. His movements are wrong somehow, and a cold chill runs down your spine as you " +
                    "realize it isn't Gil at all...\n\n    “I've been waiting all night for the right moment " +
                    "to talk to you, brother.” You turn to face the voice and are horrified at what you see staring back at you. The face " +
                    "seems to be an amalgram of Gil and your brother, but the mouth is stretched into an impossibly wide smile, and you " +
                    "can see sharp teeth like a dog's.\n\n    You weren't sure what you expected to see when you found your brother, but it certainly wasn't this.";
            response.text = "1. “And here I was expecting an ACTUAL wolfman...”\n" +
                        "2. “What did you do with the bartender?”\n" +
                        "3. “I'm here to make you answer for all the lives you've taken.” [Draw Sword] \n";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.brotherWolfman; }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { CurrentState = States.brotherBartender; }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { CurrentState = States.brotherThreat; }
        } else {
            name.text = "Location: \nCommon Room";
            text.text = "    You enjoy the soft chair and the fire for a couple minutes before you are aware of Gil, the bartender, " +
                    "taking the seat next to you. But his movements are wrong somehow, and a cold chill runs down your spine as you " +
                    "realize it isn't Gil at all...\n\n    “I've been waiting all night for the right moment " +
                    "to talk to you, brother.” You turn to face the voice and are horrified at what you see staring back at you. The face " +
                    "seems to be an amalgram of Gil and your brother, but the mouth is stretched into an impossibly wide smile, and you " +
                    "can see sharp teeth like a dog's.\n\n    You weren't sure what you expected to see when you found your brother, but it certainly wasn't this.";
            response.text = "1. “And here I was expecting an ACTUAL wolfman...”\n" +
                        "2. “What did you do with the bartender?”\n" +
                        "3. “I'm here to make you answer for all the lives you've taken.” [Draw Sword] \n";
            if (Input.GetKeyDown(KeyCode.Alpha1))          { CurrentState = States.brotherWolfman; }
            else if (Input.GetKeyDown(KeyCode.Alpha2))     { CurrentState = States.brotherBartender; }
            else if (Input.GetKeyDown(KeyCode.Alpha3))     { CurrentState = States.brotherThreat; }
        }
    }
	
	void BrotherWolfman () {
        name.text = "Talking to:\nYour Brother";
		text.text = "    “Wolfman?” Your brother cackles at the thought, “It's a convenient form, certainly, but it is only the basest version of what I can be.”\n\n    " +
                    "As he talks, your brother's face contorts in impossible ways, briefly taking on the features of a number of his past victims.\n\n    " +
                    "“I am no longer a creature of mere flesh and blood.”";
        response.text = "1. “Is my brother even in there, anymore?”\n" +
                        "2. “Whatever you are, I'm here to end you.” [Draw Sword]\n";
        if      (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.brotherInThere; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { CurrentState = States.brotherThreat; }
    }
	
	void BrotherBartender() {
        name.text = "Talking to:\nYour Brother";
        text.text = "    “Oh, the fool was quite tasty,” the impossible smile seems to increase in size and the monster licks its lips, “I can't say " +
                    "I relish taking his form, but to be here with you... it's all worth it.”\n\n    " +
                    "he must have noticed your horrified expression, because he suddenly seems obligated to defend himself. “Don't look at me like that! I'm no longer a mere mortal. " +
                    "I have a hunger, and it must be filled, lest I devolve into merely another beast.”";
		response.text = "1. “You're losing yourself, brother. Can't you see it?”\n" +
						"2. “You already ARE a beast, and I'm here to put you down.” [Draw Sword]\n";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{CurrentState = States.brotherInThere;}
		else if (Input.GetKeyDown (KeyCode.Alpha2)) 		{CurrentState = States.brotherThreat;}
	}

    void BrotherThreat() {
        if ((chrisRecruit == 2) || (jessRecruit == 2) || (drakeRecruit == 2) || (danielaRecruit == 2) || (gantRecruit == 2))
        {
            text.text = "    As your sword clears its sheath, your brother jumps back several paces and his face takes on more wolf-like qualities, " +
                        "but that infernal smile never leaves his face.\n\n    “Done talking already?” the beast seems amused at your threat, “Do you " +
                        "really think you can take me down all by yourself?”\n\n    Your hands are shaking, and you realize you no longer have any idea what you are dealing with.";
            response.text = "1. “I have to try.” [Attack]\n" +
                            "2. “Perhaps not” [Lower Sword]\n";
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                attemptAttack = true;
                CurrentState = States.brotherSword;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { CurrentState = States.brotherSword; }
        } else
        {
            text.text = "    As your sword clears its sheath, your brother jumps back several paces and his face takes on more wolf-like qualities, " +
                        "but that infernal smile never leaves his face.\n\n    “Done talking already?” the beast seems amused at your threat, “Do you " +
                        "really think you can take me down all by yourself?”\n\n    Your hands are shaking, and you realize you no longer have any idea what you are dealing with.";
            response.text = "1. “I have to try.” [Attack]";
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                attemptAttack = true;
                CurrentState = States.brotherSword;
            }
        }
	}

    void BrotherSword() {
        if (attemptAttack == true)
        {
            text.text = "    You are quick with a sword, but the beast is faster. He dodges your first swing and manages to grab hold of your wrist before you can attempt a second swing. " +
                        "Before you have time to process what happened, you are on the ground. The sword flies from your hands, and your brother leaps on top of you, pinning you against the floor.\n\n" +
                        "    He seems more beast than man now, with wild eyes and hair on his face that you could swear wasn't there before. You close your " +
                        "eyes against whatever grisly death awaits you, but it never arrives.\n\n    “Oh no, brother. We WILL settle this, but not here... " +
                        "not yet.” he growls.";
            response.text = "1. “What did you have in mind?”";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.brotherEnd; }
        }
        else
        {
            text.text = "    You carefully lower your sword, not breaking eye contact with the beast.\n\n    “Well chosen, brother. We WILL settle this, but not here... " +
                       "not yet.” he growls.";
            response.text = "1. “What did you have in mind?”";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.brotherEnd; }
        }
    }

    void BrotherInThere() {
        text.text = "    The hideous aspect of his face seems to recede a little and, for the first time, you feel like you are looking at your brother and not a " +
                    "monster.\n\n    “I don't think you understand.” your brother says with something almost like sadness, “I am not the man I was, I have transcended that old shell.” " +
                    "His face returns to the hideous amalgram of man and beast as he adds suggestively, “You could join me, of course. You too could transcend your " +
                    "simple mortality.”\n\n    The very thought of being like him disgusts you.";
        response.text = "1. “I've heard enough.” [Draw Sword]";
        if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.brotherThreat; }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { CurrentState = States.brotherThreat; }
    }

    void TheoMutter () {
		text.text = "Theo seems incoherent and ignores you.";
		response.text = "1. [Back away]";
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if 		(commonRoomRounds == 2)			{CurrentState = States.common2_1;}
			else if (commonRoomRounds == 3)			{CurrentState = States.common3;}
			else if (commonRoomRounds == 4)			{CurrentState = States.common4;}
			else if (commonRoomRounds == 5)			{CurrentState = States.common5;}
		}
	}
	
	void BrotherEnd () {
        if ((chrisRecruit == 2) || (jessRecruit == 2) || (drakeRecruit == 2) || (danielaRecruit == 2) || (gantRecruit == 2))
        {
            text.text = "    “You made some friends tonight,” your brother says condescendingly, “Take them with you and find me in the forest. We'll " +
                        "play some cat and mouse and then, who knows! If you all survive long enough to face me, it might even be a fair fight.”\n\n    " +
                        "Before you can reply, he rushes for the door. His movements are silent and so quick that you can barely follow them. As he " +
                        "leaves you can hear him call out to you.\n\n    “Happy hunting, brother.” that last phrase chills you to the bone.";
            response.text = "1. [Try to sleep]\n";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.goToBed; }
        }
        else
        {
            text.text = "    “You're a resourceful man,” your brother says condescendingly, “Find me in the forest. We'll " +
                        "play some cat and mouse and then, who knows! If you survive long enough to face me, I might even get to kill you myself.”\n\n    " +
                        "Before you can reply, he rushes for the door. His movements are silent and so quick that you can barely follow them. As he " +
                        "leaves you can hear him call out to you.\n\n    “Happy hunting, brother.” that last phrase chills you to the bone.";
            response.text = "1. [Try to sleep]\n";
            if (Input.GetKeyDown(KeyCode.Alpha1)) { CurrentState = States.goToBed; }
        }
    }
	
	void GoToBed () {
		name.text = "Location: \nJolly Baler Inn";
		text.text = "    You doubt you will be able to sleep tonight, but you still try. As you lie in bed, the adrenaline of your last encounter " +
                    "flows through your veins and you replay the details over and over again. \n\n    You couldn't kill him, yet he left you alive - " +
                    "seemingly for his own amusement. You wonder for a moment if your quest was doomed from the start, but push the thought from your " +
                    "mind.\n\n    If he is not stopped, the monster who was once your brother will only continue to kill and grow in power until no one " +
                    "could possibly stop him. For the sake of everyone, you have to succeed.\n\n    At any cost...";
		response.text = "1. To be Continued [Start Over]";
		if 		(Input.GetKeyDown (KeyCode.Alpha1)) 		{Start ();}
	}
    #endregion
}