<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class produkt extends Model
{
    use HasFactory;

    public $table = "produkter";

    public function produktgruppe() 
    {
        return $this->hasOne(produktgruppe::class);
    }
    public function linjer() 
    {
        return $this->hasMany(linje::class);
    }
}
