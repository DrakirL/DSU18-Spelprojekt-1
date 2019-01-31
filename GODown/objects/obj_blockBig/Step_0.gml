//always fall 'down', unless supported by terrain
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
			if (fallSpeed > CRUSHING_SPEED && instance_place(x, y + 1, obj_terrainCracked) != noone)
			{
				var terrain = instance_place(x, y + fallSpeed, obj_terrainCracked);
				
				with(terrain)
				{
					visible = false;
					solid = false;
					
					var terrain2 = instance_place(x + sprite_width, y, obj_terrainCracked);
					var terrain3 = instance_place(x - sprite_width, y, obj_terrainCracked);
					with(terrain2)
					{
						visible = false;
						solid = false;
					}
					with(terrain3)
					{
						visible = false;
						solid = false;
					}
				}
			}
			
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
			//WIP, iffy code
			if (fallSpeed > CRUSHING_SPEED && instance_place(x, y - 1, obj_terrainCracked) != noone)
			{
				var terrain = instance_place(x, y - fallSpeed, obj_terrainCracked);
				
				with(terrain)
				{
					visible = false;
					solid = false;
					
					var terrain2 = instance_place(x + sprite_width, y, obj_terrainCracked);
					var terrain3 = instance_place(x - sprite_width, y, obj_terrainCracked);
					with(terrain2)
					{
						visible = false;
						solid = false;
					}
					with(terrain3)
					{
						visible = false;
						solid = false;
					}
				}
			}
			
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
			if (fallSpeed > CRUSHING_SPEED && instance_place(x + 1, y , obj_terrainCracked) != noone)
			{
				var terrain = instance_place(x + fallSpeed, y, obj_terrainCracked);
				
				with(terrain)
				{
					visible = false;
					solid = false;
					
					var terrain2 = instance_place(x, y + sprite_width, obj_terrainCracked);
					var terrain3 = instance_place(x, y - sprite_width, obj_terrainCracked);
					with(terrain2)
					{
						visible = false;
						solid = false;
					}
					with(terrain3)
					{
						visible = false;
						solid = false;
					}
				}
			}
			
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
			if (fallSpeed > CRUSHING_SPEED && instance_place(x - 1, y, obj_terrainCracked) != noone)
			{
				var terrain = instance_place(x - fallSpeed, y, obj_terrainCracked);
				
				with(terrain)
				{
					visible = false;
					solid = false;
					
					var terrain2 = instance_place(x, y + sprite_width, obj_terrainCracked);
					var terrain3 = instance_place(x, y - sprite_width, obj_terrainCracked);
					with(terrain2)
					{
						visible = false;
						solid = false;
					}
					with(terrain3)
					{
						visible = false;
						solid = false;
					}
				}
			}
			
			fallSpeed = defaultFallspeed;
		}
	break;
}