<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:sanctum')->get('/user', function (Request $request) {
    return $request->user();
});

Route::group(['prefix' => 'v1', 'namespace' => 'App\Http\Controllers\Api\V1'], function()
{
    Route::apiResource('brugere', BrugerController::class);
    Route::apiResource('ekspedienter', EkspedientController::class);
    Route::apiResource('firma', FirmaController::class);
    Route::apiResource('linjer', LinjeController::class);
    Route::apiResource('ordre', OrdreController::class);
    Route::apiResource('produkter', ProdukterController::class);
    Route::apiResource('produktgrupper', ProduktgrupperController::class);
});