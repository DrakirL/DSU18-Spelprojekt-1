//works with both sizes of blocks
if(place_meeting(x, y, obj_blockBig) ||  place_meeting(x, y, obj_blockSmall) )
{
	for(var i = 0; i < instance_number(obj_terrain); i++)
	{
		var terrain = instance_find(obj_terrain, i);
	
		if(terrain.doorcode == doorcode && terrain.visible && terrain.solid)
		{
			terrain.visible = false;
			terrain.solid = false;
		}	
	}	
}
else
{
	for(var i = 0; i < instance_number(obj_terrain); i++)
	{
		var terrain = instance_find(obj_terrain, i);
	
		if(terrain.doorcode == doorcode && !terrain.visible && !terrain.solid)
		{
			terrain.visible = true;
			terrain.solid = true;
		}
	
	}	
}