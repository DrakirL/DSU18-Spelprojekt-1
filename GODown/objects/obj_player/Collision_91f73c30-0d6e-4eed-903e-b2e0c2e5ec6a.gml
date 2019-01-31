if(other.fallSpeed >= CRUSHING_SPEED)
{
	obj_game.died = true;
	obj_game.deathReason = CRUSHED_BY_BLOCK;
	global.deaths++;
	instance_destroy();	
}