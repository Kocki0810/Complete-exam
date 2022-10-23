<?php

namespace App\Http\Controllers\Api\V1;

use App\Http\Requests\StoreekspedientRequest;
use App\Http\Requests\UpdateekspedientRequest;
use App\Models\ekspedient;
use App\Http\Controllers\Controller;
use App\Http\Resources\V1\EkspedientResource;
use App\Http\Resources\V1\EkspedientCollection;


class EkspedientController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        return new EkspedientCollection(ekspedient::paginate());
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
     * @param  \App\Http\Requests\StoreekspedientRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(StoreekspedientRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\ekspedient  $ekspedient
     * @return \Illuminate\Http\Response
     */
    public function show(ekspedient $ekspedient)
    {
        return new EkspedientResource($ekspedient);
        
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\ekspedient  $ekspedient
     * @return \Illuminate\Http\Response
     */
    public function edit(ekspedient $ekspedient)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\UpdateekspedientRequest  $request
     * @param  \App\Models\ekspedient  $ekspedient
     * @return \Illuminate\Http\Response
     */
    public function update(UpdateekspedientRequest $request, ekspedient $ekspedient)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\ekspedient  $ekspedient
     * @return \Illuminate\Http\Response
     */
    public function destroy(ekspedient $ekspedient)
    {
        //
    }
}
