<?php

namespace App\Http\Controllers;

use App\Http\Requests\StorelinjeRequest;
use App\Http\Requests\UpdatelinjeRequest;
use App\Models\linje;

class LinjeController extends Controller
{
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        //
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
     * @param  \App\Http\Requests\StorelinjeRequest  $request
     * @return \Illuminate\Http\Response
     */
    public function store(StorelinjeRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  \App\Models\linje  $linje
     * @return \Illuminate\Http\Response
     */
    public function show(linje $linje)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  \App\Models\linje  $linje
     * @return \Illuminate\Http\Response
     */
    public function edit(linje $linje)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \App\Http\Requests\UpdatelinjeRequest  $request
     * @param  \App\Models\linje  $linje
     * @return \Illuminate\Http\Response
     */
    public function update(UpdatelinjeRequest $request, linje $linje)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  \App\Models\linje  $linje
     * @return \Illuminate\Http\Response
     */
    public function destroy(linje $linje)
    {
        //
    }
}
