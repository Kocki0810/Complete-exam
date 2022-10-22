<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class ekspedient extends Model
{
    use HasFactory;
    
    public $table = "ekspedienter";

    public function firma() 
    {
        return $this->hasOne(firma::class);
    }
    public function bruger()
    {
        return $this->hasOne(bruger::class);
    }
    public function ordre() 
    {
        return $this->hasMany(ordre::class);
    }
    
}
