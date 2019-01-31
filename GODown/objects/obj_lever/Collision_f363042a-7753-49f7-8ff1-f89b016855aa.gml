if(keyboard_check_pressed(vk_space) && obj_player.fallSpeed == PLAYER_FALLSPEED)
{
	if(image_index == 0)
	{
		image_index = 1;
		for(var i = 0; i < instance_number(obj_terrain); i++)
		{
			var terrain = instance_find(obj_terrain, i);
			if(terrain.doorcode == doorcode)
			{
				terrain.visible = false;
				terrain.solid = false;
			}
		}
		for(var i = 0; i < instance_number(obj_terrainInvisible); i++)
		{
			var terrain = instance_find(obj_terrainInvisible, i);
			if(terrain.doorcode == doorcode)
			{
				terrain.visible = true;
				terrain.solid = true;
			}
		}
	}
	else
	{
		image_index = 0;
		for(var i = 0; i < instance_number(obj_terrain); i++)
		{
			var terrain = instance_find(obj_terrain, i);
			if(terrain.doorcode == doorcode)
			{
				terrain.visible = true;
				terrain.solid = true;	
			}
		}
		for(var i = 0; i < instance_number(obj_terrainInvisible); i++)
		{
			var terrain = instance_find(obj_terrainInvisible, i);
			if(terrain.doorcode == doorcode)
			{
				terrain.visible = false;
				terrain.solid = false;	
			}
		}
	}
}