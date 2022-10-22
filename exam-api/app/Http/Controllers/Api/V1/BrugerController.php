<?php

namespace App\Http\Controllers\Api\V1;

use App\Http\Requests\StorebrugerRequest;
use App\Http\Requests\UpdatebrugerRequest;
use App\Models\bruger;
use App\Http\Resources\V1\BrugerResource;
use App\Http\Controllers\Controller;

class BrugerController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        return bruger::all();
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \App\Http\Requests\StorebrugerRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(StorebrugerRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\bruger  $bruger
     * @return \Illuminate\Http\Response
     */
    public function show(bruger $bruger)
    {
        return new BrugerResource($bruger);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\bruger  $bruger
     * @return \Illuminate\Http\Response
     */
    public function edit(bruger $bruger)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\UpdatebrugerRequest  $request
     * @param  \App\Models\bruger  $bruger
     * @return \Illuminate\Http\Response
     */
    public function update(UpdatebrugerRequest $request, bruger $bruger)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\bruger  $bruger
     * @return \Illuminate\Http\Response
     */
    public function destroy(bruger $bruger)
    {
        //
    }
}
