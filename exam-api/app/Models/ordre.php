<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class ordre extends Model
{
    use HasFactory;

    public $table = "ordre";


    public function firma() 
    {
        return $this->hasOne(firma::class);
    }
    public function linjer() 
    {
        return $this->hasMany(linje::class);
    }
    public function ekspedienter() 
    {
        return $this->hasMany(ekspedient::class);
    }
}
