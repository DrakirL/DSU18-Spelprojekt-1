//always fall 'down', unless supported by terrain
if(obj_game.died)
{
	carried = false;	
}

if(carried)
{
	x = obj_player.x;
	y = obj_player.y;
}
else
{
	switch(global.currentGravityDirection)
	{
		case DOWN:
			//if nothing is below
			if( place_free(x, y + 1) )
			{
				move_contact_solid(global.currentGravityDirection - 90, fallSpeed);
			
				if(fallSpeed < MAX_FALLSPEED)
					fallSpeed *= GRAVITY_ACCELERATION;
			}
			else
			{
				fallSpeed = defaultFallspeed;
			}
		break;
		case UP:
			//if nothing is above
			if( place_free(x, y - 1) )
			{
				move_contact_solid(global.currentGravityDirection - 90, fallSpeed);
			
				if(fallSpeed < MAX_FALLSPEED)
					fallSpeed *= GRAVITY_ACCELERATION;
			}
			else
			{	
				fallSpeed = defaultFallspeed;
			}
		break;
		case RIGHT:
			//if nothing is to the right
			if( place_free(x + 1, y) )
			{
				move_contact_solid(global.currentGravityDirection - 90, fallSpeed);
			
				if(fallSpeed < MAX_FALLSPEED)
					fallSpeed *= GRAVITY_ACCELERATION;	
			}
			else
			{
				fallSpeed = defaultFallspeed;
			}
		break;
		case LEFT:
		//if nothing is to the right
			if( place_free(x - 1, y) )
			{
				move_contact_solid(global.currentGravityDirection - 90, fallSpeed);
			
				if(fallSpeed < MAX_FALLSPEED)
					fallSpeed *= GRAVITY_ACCELERATION;	
			}
			else
			{
				fallSpeed = defaultFallspeed;
			}
		break;
	}	
}