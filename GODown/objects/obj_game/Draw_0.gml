if(room != rm_win)
{
	if(died)
	{
		draw_text(x - 30, y + 60, @"YOU DIED!
				Press R to restart level");
		/*
		switch(deathReason)
		{
			case CRUSHED_BY_BLOCK:
				draw_text(x - 30, y + 60, @"YOU DIED! Reason: Crushed by a block
				Press R to restart level");	
			break;
			
			case CRUSHED_BY_WALL:
				draw_text(x - 30, y + 60, @"YOU DIED! Reason: Crushed by a wall
				Press R to restart level");	
			break;
			
			case FELL:
				draw_text(x - 30, y + 60, @"YOU DIED! Reason: Fell too far, too quickly, too much
				Press R to restart level");	
			break;
			default:
				draw_text(x - 30, y + 60, @"YOU DIED! Reason: ?
				Press R to restart level");	
			break;
		}
		*/
	}
}

if(room == rm_win)
{
	draw_text(x, y, "WOW, you beat the entire game");	
}

if(room == rm_level0)
{
	draw_text(1280, 781, @"Arrowkeys to change gravity direction
	A and D to move sideways
	R to reset level
	press TAB to toggle extra info
	SPACEBAR to interact");	
	
}