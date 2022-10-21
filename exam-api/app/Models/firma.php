<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class firma extends Model
{
    use HasFactory;

    public $table = "firma";

    public function brugere() 
    {
        return $this->hasMany(bruger::class);
    }
    public function ekspedienter() 
    {
        return $this->hasMany(ekspedient::class);
    }

    public function produktgrupper() 
    {
        return $this->hasMany(produktgruppe::class);
    }
    
    public function produkter() 
    {
        return $this->hasMany(produkt::class);
    }

    public function ordre() 
    {
        return $this->hasMany(ordre::class);
    }
}
