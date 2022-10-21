<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class bruger extends Model
{
    use HasFactory;

    public $table = "brugere";
    
    public function firma()
    {
        $this->hasOne(firma::class);
    }
    public function ekspedient()
    {
        $this->hasOne(ekspedient::class);
    }
    
}
