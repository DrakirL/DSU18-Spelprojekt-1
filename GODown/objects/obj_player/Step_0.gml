if( !place_free(x, y) )
{
	obj_game.died = true;
	obj_game.deathReason = CRUSHED_BY_WALL;
	global.deaths++;
	instance_destroy();	
}

//move left or right relative to diretion of gravity
#region move
if( keyboard_check(ord("D")) || keyboard_check(ord("A")) )
{
	switch(global.currentGravityDirection)
	{
		case DOWN:
			if(keyboard_check(ord("D")) && place_free(x + 1, y) )
			{
				move_contact_solid(0, PLAYER_MOVESPEED);
			}
			else if(keyboard_check(ord("A")) && place_free(x - 1, y) )
			{
				move_contact_solid(180, PLAYER_MOVESPEED);
			}
		break;
	
		case RIGHT:
			if(keyboard_check(ord("D")) && place_free(x, y - 1) )
			{
				move_contact_solid(90, PLAYER_MOVESPEED);
			}
			else if(keyboard_check(ord("A")) && place_free(x, y + 1) )
			{
				move_contact_solid(270, PLAYER_MOVESPEED);
			}
		break;
	
		case UP:
			if(keyboard_check(ord("D")) && place_free(x - 1, y) )
			{
				move_contact_solid(180, PLAYER_MOVESPEED);
			}
			else if(keyboard_check(ord("A")) && place_free(x + 1, y) )
			{
				move_contact_solid(0, PLAYER_MOVESPEED);
			}
		break;
	
		case LEFT:
			if(keyboard_check(ord("D")) && place_free(x, y + 1) )
			{
				move_contact_solid(270, PLAYER_MOVESPEED);
			}
			else if(keyboard_check(ord("A")) && place_free(x, y - 1) )
			{
				move_contact_solid(90, PLAYER_MOVESPEED);
			}
		break;
	}
}

#endregion

//always fall 'down', unless supported by terrain
#region fall
switch(global.currentGravityDirection)
{
	case DOWN:
		image_angle = DOWN;
		//if nothing is below
		if( place_free(x, y + 1) )
		{
			move_contact_solid(global.currentGravityDirection - 90, fallSpeed);
			
			if(fallSpeed < MAX_FALLSPEED)
			fallSpeed *= GRAVITY_ACCELERATION;
		}
		else
		{
			if(fallSpeed > LETHAL_FALLSPEED)
			{
				obj_game.died = true;
				obj_game.deathReason = FELL;
				global.deaths++;
				instance_destroy();
			}
			else
				fallSpeed = PLAYER_FALLSPEED;
					
			fallSpeed = PLAYER_FALLSPEED;
		}
	break;
	case UP:
		image_angle = UP;
		//if nothing is above
		if( place_free(x, y - 1) )
		{
			move_contact_solid(global.currentGravityDirection - 90, fallSpeed);
			
			if(fallSpeed < MAX_FALLSPEED)
			fallSpeed *= GRAVITY_ACCELERATION;
		}
		else
		{
			if(fallSpeed > LETHAL_FALLSPEED)
			{
				obj_game.died = true;
				obj_game.deathReason = FELL;
				global.deaths++;
				instance_destroy();
			}
			else
				fallSpeed = PLAYER_FALLSPEED;
		}
	break;
	case RIGHT:
		image_angle = RIGHT;
		//if nothing is to the right
		if( place_free(x + 1, y) )
		{
			move_contact_solid(global.currentGravityDirection - 90, fallSpeed);
			
			if(fallSpeed < MAX_FALLSPEED)
			fallSpeed *= GRAVITY_ACCELERATION;	
		}
		else
		{
			if(fallSpeed > LETHAL_FALLSPEED)
			{
				obj_game.died = true;
				obj_game.deathReason = FELL;
				global.deaths++;
				instance_destroy();
			}
			else
				fallSpeed = PLAYER_FALLSPEED;
		}
	break;
	case LEFT:
		image_angle = LEFT;
	//if nothing is to the left
		if( place_free(x - 1, y) )
		{
			move_contact_solid(global.currentGravityDirection - 90, fallSpeed);
			
			if(fallSpeed < MAX_FALLSPEED)
			fallSpeed *= GRAVITY_ACCELERATION;	
		}
		else
		{
			if(fallSpeed > LETHAL_FALLSPEED)
			{
				obj_game.died = true;
				obj_game.deathReason = FELL;
				global.deaths++;
				instance_destroy();
			}
			else
				fallSpeed = PLAYER_FALLSPEED;
			
		}
	break;
}

#endregion